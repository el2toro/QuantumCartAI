using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class PaymentAddedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public PaymentId PaymentId { get; }
    public decimal Amount { get; }
    public PaymentAddedEvent(OrderId orderId, PaymentId paymentId, decimal amount)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
    }
}
