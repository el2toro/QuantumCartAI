using MassTransit;
using Ordering.Domain.Events;

namespace Ordering.Application.Handlers.Events;

public class OrderDraftCreatedEventHandler : IConsumer<OrderDraftCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderDraftCreatedEvent> context)
    {
        var data = context.Message;
        //TODO: Reserve stock/inventory
        //1. Call CatalogService for that
        //2. Send notifications email/sms
        //Call NotificationService
        //3. Add entry to OrderStatusHistory
        return Task.CompletedTask;
    }
}
