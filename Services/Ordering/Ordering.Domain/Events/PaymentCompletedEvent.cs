using Ordering.Domain.ValueObjects;
using SharedKernel.Common;

namespace Ordering.Domain.Events;

public class PaymentCompletedEvent : DomainEvent
{
    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public decimal Amount { get; }

    public PaymentCompletedEvent(PaymentId paymentId, OrderId orderId, decimal amount)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
    }
}
