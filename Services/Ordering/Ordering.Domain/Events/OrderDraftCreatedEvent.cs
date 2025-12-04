using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class OrderDraftCreatedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string OrderNumber { get; }

    public OrderDraftCreatedEvent(OrderId orderId, CustomerId customerId, string orderNumber)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderNumber = orderNumber;
    }
}
