using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MassTransit;
using Cart.Domain.Events;
using Cart.Application.Dtos;
using BuildingBlocks.CQRS;
using Cart.Application.Interfaces;

namespace Cart.Application.Handlers.Commands;


public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity, Currency Currency) : ICommand<AddItemResult>;
public record AddItemResult(CartDto Cart);

public class AddItemHandler(IDistributedCache distributedCache,
    IPublishEndpoint publisher, ICatalogGrpcService catalogGrpcService)
    : ICommandHandler<AddItemCommand, AddItemResult>
{
    public async Task<AddItemResult> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        // Create a cart instance if CustomerId/CartId is not provided       
        var cart = command.CustomerId.HasValue
             ? await GetCachedCart(command.CustomerId.ToString()!, command.Currency)
             : CreateNewCart(CustomerId.From(command.CustomerId.ToString()!), command.Currency);

        // 1. Validate ProductId (calling CatalogService)
        //var productQuery = await catalogGrpcService.GetProduct(command.ProductId);

        //if (!productQuery.ProductExists)
        //{
        //    return new AddItemResult(new CartDto());
        //}

        // 3. Add item to domain aggregate
        cart.AddItem(
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity),
            new Money(50));

        var cartDto = CreateCartDto(cart);

        // 2. Call Pricing Service to get current price (applied Discount)        

        // 4. Save the cart (Redis/Mongo/etc.)
        await CacheCartDto(cartDto);

        // 5. Publish event item added to cart
        await publisher.Publish(new CartItemAddedEvent(cart.Id,
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity), new Money(50)),
            cancellationToken);

        return new AddItemResult(cartDto);
    }

    private async Task<Domain.Entities.Cart> GetCachedCart(string cartId, Currency currency)
    {
        var cachedCartJson = await distributedCache.GetStringAsync(cartId);
        var cartDto = JsonSerializer.Deserialize<CartDto>(cachedCartJson!)!;
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
        new CartItemDto
        {
            ProductId = item.ProductId.Value,
            Quantity = item.Quantity.Value,
            DiscountedPrice = item.UnitPrice.Amount,
            Price = item.UnitPrice.Amount
        });

        return new CartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId?.Value,
            CartItems = cartItems,
            Subtotal = cart.Subtotal.Amount
        };
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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            });
    }
}
