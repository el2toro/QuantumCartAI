using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MassTransit;
using Cart.Domain.Events;
using Cart.Application.Dtos;
using BuildingBlocks.CQRS;
using Cart.Application.Interfaces;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;

namespace Cart.Application.Handlers.Commands;

public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity, decimal Price, Currency Currency) : ICommand<AddItemResult>;
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
        CustomerId customerId = CustomerId.From(sessionId.ToString()!);
        var cart = await GetCachedCart(customerId, command.Currency)
                 ?? CreateNewCart(customerId, command.Currency);

        // 1. Validate ProductId (calling CatalogService)
        var productInStock = await catalogGrpcService.GetProduct(command.ProductId);

        // 2. Call Pricing Service to get current price (applied Discount)
        // 
        // var discounts = await discountGrpcService.GetProductDiscounts(command.ProductId);

        if (productInStock.ProductExists)
        {
            // 3. Add item to domain aggregate
            cart.AddItem(
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity),
            new Money(command.Price));
        }

        var cartDto = CreateCartDto(cart);

        // 4. Save the cart (Redis/Mongo/etc.)
        await CacheCartDto(cartDto);

        // 5. Publish event item added to cart
        await publisher.Publish(new CartItemAddedEvent(cart.Id,
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity), new Money(50)),
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
                new Money(cartItem.Price));
        }

        return cart;
    }

    private CartDto CreateCartDto(Domain.Entities.Cart cart)
    {
        var cartItems = cart.Items.Select(item =>
        new CartItemDto(item.ProductId.Value, item.Quantity.Value, item.UnitPrice.Amount, item.UnitPrice.Amount));

        return new CartDto(cart.Id, cart.CustomerId?.Value, cartItems, cart.Subtotal.Amount, cart.Subtotal.Amount);
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
}
