using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class OrderShippedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string TrackingNumber { get; }
    public string Carrier { get; }
    public DateTime ShippedDate { get; }

    public OrderShippedEvent(OrderId orderId, CustomerId customerId, string trackingNumber, string carrier, DateTime shippedDate)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TrackingNumber = trackingNumber;
        Carrier = carrier;
        ShippedDate = shippedDate;
    }
}

