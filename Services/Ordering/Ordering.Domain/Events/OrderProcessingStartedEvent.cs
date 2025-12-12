using Ordering.Domain.ValueObjects;
using SharedKernel.Common;

namespace Ordering.Domain.Events;

public class OrderProcessingStartedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string OrderNumber { get; }
    public OrderProcessingStartedEvent(OrderId orderId, CustomerId customerId, string orderNumber)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderNumber = orderNumber;
    }
}
