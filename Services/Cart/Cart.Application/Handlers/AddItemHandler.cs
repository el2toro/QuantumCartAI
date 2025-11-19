using Cart.Application.Commands;
using MediatR;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MassTransit;
using Cart.Domain.Events;

namespace Cart.Application.Handlers;

public class AddItemHandler(IDistributedCache distributedCache, IPublishEndpoint publisher)
    : IRequestHandler<AddItemCommand, AddItemResult>
{
    public async Task<AddItemResult> Handle(AddItemCommand command, CancellationToken cancellationToken)
    {
        // Create a cart instance if CustomerId is not provided


        var cart = new Domain.Entities.Cart(CartId.New(), null!);
        // 1. Validate ProductId
        // 2. Call Pricing Service to get current price




        // 3. Add item to domain aggregate
        cart.AddItem(
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity),
            new Money(50));

        // 4. Save the cart (Redis/Mongo/etc.)
        distributedCache.SetString(
            cart.Id.ToString(),
            JsonSerializer.Serialize(cart),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

        // 5. Publish event item added to cart
        await publisher.Publish(new CartItemAddedEvent(cart.Id,
            ProductId.From(command.ProductId.ToString()),
            Quantity.From(command.Quantity), new Money(50)),
            cancellationToken);

        return new AddItemResult(true);
    }
}
