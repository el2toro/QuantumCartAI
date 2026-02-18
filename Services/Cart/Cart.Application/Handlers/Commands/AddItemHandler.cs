using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Application.Interfaces;
using Cart.Domain.Events;
using Cart.Domain.Exceptions;
using Cart.Domain.ValueObjects;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;
using System.Text.Json;

namespace Cart.Application.Handlers.Commands;

public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity) : ICommand<AddItemResult>;
public record AddItemResult(CartDto Cart);

public class AddItemHandler(IDistributedCache distributedCache,
    IPublishEndpoint publisher,
    ICatalogGrpcService catalogGrpcService,
    IDiscountGrpcService discountGrpcService,
    CurrentSession currentSession)
    : ICommandHandler<AddItemCommand, AddItemResult>
{
    public async Task<AddItemResult> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        var sessionId = currentSession.IsAuthenticated
                      ? currentSession.AuthenticatedUserId
                      : currentSession.AnonymousId;

        // Create a cart instance if CustomerId/CartId is not provided
        // Currency can be determined based on user preferences or location (for simplicity, using USD here)
        CustomerId customerId = CustomerId.From(sessionId.ToString()!);
        var cart = await GetCachedCart(customerId, Currency.USD)
                 ?? CreateNewCart(customerId, Currency.USD);

        // 1. Validate Product (calling CatalogService)
        // Check if product exists and is in stock
        var product = await catalogGrpcService.GetProduct(command.ProductId);

        // Check if the product is already in the cart and compare the product quantity with available stock
        // If the product is already in the cart, we need to consider the existing quantity in the cart when validating stock availability
        // For example, if the product has a stock quantity of 10 and the user already has 2 in the cart, they should only be able to add up to 8 more
        // If the product is not in the cart, we simply check if the requested quantity is less than or equal to the available stock
        var itemInTheCart = cart.Items.FirstOrDefault(p => p.ProductId == ProductId.From(product.ProductId.ToString()));

        bool productIsInStock = itemInTheCart is not null
                ? product.Quantity > itemInTheCart.Quantity.Value
                : product.Quantity >= command.Quantity;

        // If the product is not in stock, we can choose to either return an error or add the item with a note about stock availability.
        if (!productIsInStock)
            throw new OutOfStockException($"Product {product.ProductId} is out of stock");

        // 2. Call Pricing Service to get current price (applied Discount)
        var discounts = await discountGrpcService.GetProductDiscounts(command.ProductId);

        decimal latestPrice = discounts.Any()
            ? GetProductLatestPrice(discounts, product.Price)
            : product.Price;

        // 3. Add item to domain aggregate      
        cart.AddItem(
        ProductId.From(command.ProductId.ToString()),
        Quantity.From(command.Quantity),
        new Money(product.Price),
        new Money(latestPrice));

        var cartDto = CreateCartDto(cart);

        // 4. Save the cart (Redis/Mongo/etc.)
        await CacheCartDto(cartDto);

        // 5. Publish event item added to cart
        await publisher.Publish(new CartItemAddedEvent(cart.Id,
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity),
            new Money(product.Price),
            new Money(latestPrice)),
            cancellationToken);

        return new AddItemResult(cartDto);
    }

    private async Task<Domain.Entities.Cart?> GetCachedCart(CustomerId customerId, Currency currency)
    {
        string? cachedCartJson = await distributedCache.GetStringAsync(customerId.Value.ToString());

        if (string.IsNullOrEmpty(cachedCartJson))
        {
            return null;
        }

        var cartDto = JsonSerializer.Deserialize<CartDto>(cachedCartJson)!;
        var cart = new Domain.Entities.Cart(CartId.From(cartDto.Id.ToString()),
            CustomerId.From(cartDto.CustomerId.ToString()!),
            currency);

        foreach (var cartItem in cartDto.CartItems)
        {
            cart.AddItem(ProductId.From(cartItem.ProductId.ToString()),
                Quantity.From(cartItem.Quantity),
                new Money(cartItem.Price),
                new Money(cartItem.DiscountedPrice));
        }

        return cart;
    }

    private CartDto CreateCartDto(Domain.Entities.Cart cart)
    {
        var cartItems = cart.Items.Select(item =>
        new CartItemDto(item.ProductId.Value, item.Quantity.Value, item.UnitPrice.Amount, item.DiscountedPrice.Amount));

        return new CartDto(cart.Id, cart.CustomerId?.Value, cartItems, cart.Subtotal.Amount, cart.Total.Amount);
    }

    private Domain.Entities.Cart CreateNewCart(CustomerId customerId, Currency currency)
    {
        return new Domain.Entities.Cart(CartId.New(), customerId, currency);
    }

    private async Task CacheCartDto(CartDto cartDto)
    {
        await distributedCache.SetStringAsync(
            cartDto.CustomerId.ToString(),
            JsonSerializer.Serialize(cartDto),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
            });
    }

    private decimal GetProductLatestPrice(IEnumerable<AppliedDiscountDto> appliedDiscounts, decimal productPrice)
    {
        decimal totalDiscount = 0;
        // Get total discount amount (considering both percentage and fixed discounts)
        foreach (var discount in appliedDiscounts)
        {
            if (discount.IsPercentage)
                totalDiscount = productPrice * ((decimal)discount.Amount / 100);

            else totalDiscount = discount.Amount;
        }

        return productPrice - totalDiscount;
    }
}
