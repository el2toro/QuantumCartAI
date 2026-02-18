using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Application.Handlers.Commands;

public record RemoveItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity) : ICommand<RemoveItemResult>;
public record RemoveItemResult(CartDto Cart);

public class RemoveItemHandler(IDistributedCache distributedCache)
    : ICommandHandler<RemoveItemCommand, RemoveItemResult>
{
    public async Task<RemoveItemResult> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
    {
        // For simplicity, we are using a fixed currency.
        // In a real application, the currency would likely be determined based on the customer's preferences or location.
        var cart = await GetCachedCart(CustomerId.From(command.CustomerId.ToString()!), Currency.USD);
        cart.RemoveItem(ProductId.From(command.ProductId.ToString()), Quantity.From(command.Quantity));

        var cartDto = CreateCartDto(cart);

        await CacheCartDto(cartDto);

        return new RemoveItemResult(cartDto);
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

    private CartDto CreateCartDto(Domain.Entities.Cart cart)
    {
        var cartItems = cart.Items.Select(item =>
        new CartItemDto(item.ProductId.Value, item.Quantity.Value, item.UnitPrice.Amount, item.UnitPrice.Amount));

        return new CartDto(cart.Id, cart.CustomerId?.Value, cartItems, cart.Subtotal.Amount, cart.Subtotal.Amount);
    }
}


