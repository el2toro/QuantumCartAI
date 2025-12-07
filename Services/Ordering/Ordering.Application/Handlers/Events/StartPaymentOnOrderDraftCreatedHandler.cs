using MassTransit;
using Ordering.Domain.Events;

namespace Ordering.Application.Handlers.Events;

public class StartPaymentOnOrderDraftCreatedHandler : IConsumer<OrderDraftCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderDraftCreatedEvent> context)
    {
        return Task.CompletedTask;
    }
}
