using Ordering.Domain.ValueObjects;
using SharedKernel.Common;

namespace Ordering.Domain.Events;

public class OrderDeliveredEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public DateTime DeliveredDate { get; }

    public OrderDeliveredEvent(OrderId orderId, CustomerId customerId, DateTime deliveredDate)
    {
        OrderId = orderId;
        CustomerId = customerId;
        DeliveredDate = deliveredDate;
    }
}