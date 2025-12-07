using MassTransit;
using Ordering.Domain.Events;

namespace Ordering.Application.Handlers.Events;

public class SendEmailOnOrderDraftCreatedHandler : IConsumer<OrderDraftCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderDraftCreatedEvent> context)
    {
        return Task.CompletedTask;
    }
}
