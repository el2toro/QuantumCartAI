using Cart.Domain.Events;
using MassTransit;

namespace Cart.Application.Handlers.Events;

public class CartItemAddedEventHandler : IConsumer<CartItemAddedEvent>
{
    public Task Consume(ConsumeContext<CartItemAddedEvent> context)
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }
}
