using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class OrderPaidEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string PaymentId { get; }
    public decimal Amount { get; }
    public DateTime PaidDate { get; }

    public OrderPaidEvent(OrderId orderId, CustomerId customerId, string paymentId, decimal amount, DateTime paidDate)
    {
        OrderId = orderId;
        CustomerId = customerId;
        PaymentId = paymentId;
        Amount = amount;
        PaidDate = paidDate;
    }
}
