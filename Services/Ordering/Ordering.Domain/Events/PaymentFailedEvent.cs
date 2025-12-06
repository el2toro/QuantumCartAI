using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class PaymentFailedEvent : DomainEvent
{
    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public decimal Amount { get; }
    public string ErrorMessage { get; }

    public PaymentFailedEvent(PaymentId paymentId, OrderId orderId, decimal amount, string errorMessage)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
        ErrorMessage = errorMessage;
    }
}
