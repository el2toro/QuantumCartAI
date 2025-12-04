namespace Ordering.Application.DTOs;

public record OrderItemDetailsDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public string ProductImageUrl { get; init; }
    public string Sku { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTime AddedAt { get; init; }
}