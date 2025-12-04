namespace Ordering.Application.DTOs;

public record OrderStatusHistoryDto
{
    public string Status { get; init; }
    public DateTime ChangedAt { get; init; }
    public string ChangedBy { get; init; }
    public string Notes { get; init; }
}
