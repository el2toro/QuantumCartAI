using Cart.Application.Commands;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MassTransit;
using Cart.Domain.Events;
using Cart.Application.Dtos;
using BuildingBlocks.CQRS;

namespace Cart.Application.Handlers;

public class AddItemHandler(IDistributedCache distributedCache, IPublishEndpoint publisher)
    : ICommandHandler<AddItemCommand, AddItemResult>
{
    public async Task<AddItemResult> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        // Create a cart instance if CustomerId/CartId is not provided

        var cart = command.CartId.HasValue
             ? await GetCachedCart(command.CartId.ToString()!)
             : CreateNewCart();

        // 1. Validate ProductId (calling CatalogService)
        // 2. Call Pricing Service to get current price (applied Discount)


        // 3. Add item to domain aggregate
        cart.AddItem(
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity),
            new Money(50));

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

    private async Task<Domain.Entities.Cart> GetCachedCart(string cartId)
    {
        var cachedCartJson = await distributedCache.GetStringAsync(cartId);
        var cartDto = JsonSerializer.Deserialize<CartDto>(cachedCartJson!)!;
        var cart = new Domain.Entities.Cart(CartId.From(cartDto.Id.ToString()));

        foreach (var cartItem in cartDto.CartItems)
        {
            cart.AddItem(ProductId.From(cartItem.ProductId.ToString()), Quantity.From(cartItem.Quantity), new Money(cartItem.Price));
        }

        return cart;
    }

    private CartDto CreateCartDto(Domain.Entities.Cart cart)
    {
        var cartItems = cart.Items.Select(item =>
        new CartItemDto(item.ProductId.Value, item.Quantity.Value, item.UnitPrice.Amount));

        return new CartDto(cart.Id, cart.CustomerId?.Value, cartItems, cart.Subtotal.Amount);
    }

    private Domain.Entities.Cart CreateNewCart()
    {
        return new Domain.Entities.Cart(CartId.New());
    }

    private async Task CacheCartDto(CartDto cartDto)
    {
        await distributedCache.SetStringAsync(
            cartDto.Id.ToString(),
            JsonSerializer.Serialize(cartDto),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            });
    }
}
