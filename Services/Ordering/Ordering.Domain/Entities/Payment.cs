using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Payment : AggregateRoot<PaymentId>
{
    public OrderId OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }
    public string PaymentMethod { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string GatewayTransactionId { get; private set; }
    public DateTime PaymentDate { get; private set; }

    // Navigation property to Order
    public Order Order { get; private set; }

    private Payment() { }

    public static Payment Create(
        OrderId orderId,
        decimal amount,
        Currency currency,
        string paymentMethod,
        string gatewayTransactionId = null,
        string cardLastFour = null)
    {
        return new Payment
        {
            Id = PaymentId.Create(),
            OrderId = orderId,
            Amount = amount,
            Currency = currency,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Processing,
            GatewayTransactionId = gatewayTransactionId,
            PaymentDate = DateTime.UtcNow
        };
    }

    public void MarkAsPaid(string gatewayResponse = null)
    {
        Status = PaymentStatus.Paid;
    }
}
public record CardDetails
{
    public string LastFourDigits { get; init; }
    public string CardType { get; init; }
    public int ExpiryMonth { get; init; }
    public int ExpiryYear { get; init; }
}

public record PaymentAttempt(
    decimal Amount,
    string Status,
    string ErrorMessage)
{
    public DateTime AttemptDate { get; } = DateTime.UtcNow;
}
