using Ordering.Domain.ValueObjects;


namespace Ordering.Domain.Events;

public class OrderCancelledEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string Reason { get; }
    public decimal Amount { get; }

    public OrderCancelledEvent(OrderId orderId, CustomerId customerId, string reason, decimal amount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        Reason = reason;
        Amount = amount;
    }
}