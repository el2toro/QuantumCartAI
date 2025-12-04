namespace Ordering.Application.DTOs;

public record PaymentDetailsDto
{
    public string PaymentId { get; init; }
    public string PaymentMethod { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public string Status { get; init; }
    public DateTime PaymentDate { get; init; }
    public string TransactionId { get; init; }
    public string GatewayResponse { get; init; }
    public List<PaymentAttemptDto> Attempts { get; init; } = new();
}