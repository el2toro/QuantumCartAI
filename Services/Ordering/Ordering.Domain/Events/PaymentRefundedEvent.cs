using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class PaymentRefundedEvent : DomainEvent
{
    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public decimal RefundAmount { get; }
    public string Reason { get; }

    public PaymentRefundedEvent(PaymentId paymentId, OrderId orderId, decimal refundAmount, string reason)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        RefundAmount = refundAmount;
        Reason = reason;
    }
}