using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.Entities;

public class Payment : Entity
{
    public PaymentId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentStatus Status { get; private set; }
    public CardDetails CardDetails { get; private set; }
    public string GatewayTransactionId { get; private set; }
    public string GatewayResponse { get; private set; }
    public Address BillingAddress { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public DateTime? RefundDate { get; private set; }
    public decimal? RefundAmount { get; private set; }
    public string RefundReason { get; private set; }

    private readonly List<PaymentAttempt> _attempts = new();
    public IReadOnlyCollection<PaymentAttempt> Attempts => _attempts.AsReadOnly();

    private Payment() { }

    public static Payment Create(
        OrderId orderId,
        decimal amount,
        Currency currency,
        PaymentMethod paymentMethod,
        string gatewayTransactionId = null)
    {
        return new Payment
        {
            Id = PaymentId.New(),
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
        GatewayResponse = gatewayResponse;

        // AddDomainEvent(new PaymentCompletedEvent(Id, OrderId, Amount));
    }

    public void MarkAsFailed(string gatewayResponse)
    {
        Status = PaymentStatus.Failed;
        GatewayResponse = gatewayResponse;

        //   AddDomainEvent(new PaymentFailedEvent(Id, OrderId, Amount, gatewayResponse));
    }

    public void AddAttempt(decimal amount, string status, string errorMessage = null)
    {
        var attempt = new PaymentAttempt(amount, status, errorMessage);
        _attempts.Add(attempt);
    }

    public void Refund(decimal amount, string reason)
    {
        if (Status != PaymentStatus.Paid)
            // throw new OrderingDomainException("Only paid payments can be refunded");

            if (amount > Amount)
                //  throw new OrderingDomainException("Refund amount cannot exceed payment amount");

                RefundAmount = amount;
        RefundReason = reason;
        RefundDate = DateTime.UtcNow;

        if (amount == Amount)
        {
            Status = PaymentStatus.Refunded;
        }
        else
        {
            Status = PaymentStatus.PartiallyRefunded;
        }

        //   AddDomainEvent(new PaymentRefundedEvent(Id, OrderId, amount, reason));
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
