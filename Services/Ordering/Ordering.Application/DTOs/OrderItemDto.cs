namespace Ordering.Application.DTOs;

public record OrderItemDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public string ProductImageUrl { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal Discount { get; init; }
}
