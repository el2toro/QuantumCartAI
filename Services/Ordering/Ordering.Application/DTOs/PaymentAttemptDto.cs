namespace Ordering.Application.DTOs;

public record PaymentAttemptDto
{
    public DateTime AttemptDate { get; init; }
    public decimal Amount { get; init; }
    public string Status { get; init; }
    public string ErrorMessage { get; init; }
}