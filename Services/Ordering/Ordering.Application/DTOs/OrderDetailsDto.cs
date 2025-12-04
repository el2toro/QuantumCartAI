namespace Ordering.Application.DTOs;

public record OrderDetailsDto
{
    public Guid Id { get; init; }
    public string OrderNumber { get; init; }
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerName { get; init; }
    public string Status { get; init; }
    public string PaymentStatus { get; init; }
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; }
    public DateTime OrderDate { get; init; }
    public DateTime? PaidDate { get; init; }
    public DateTime? ShippedDate { get; init; }
    public DateTime? DeliveredDate { get; init; }
    public AddressDto ShippingAddress { get; init; }
    public AddressDto BillingAddress { get; init; }
    public List<OrderItemDetailsDto> Items { get; init; } = new();
    public List<OrderStatusHistoryDto> StatusHistory { get; init; } = new();
    public PaymentDetailsDto Payment { get; init; }
    public ShipmentDetailsDto Shipment { get; init; }
}
