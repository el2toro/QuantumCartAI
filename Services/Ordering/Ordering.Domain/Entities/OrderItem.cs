using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductImageUrl { get; private set; }
    public string ProductSku { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal Discount { get; private set; }

    // No navigation back to Order (unidirectional relationship)
    private OrderItem() { }

    public static OrderItem Create(
        ProductId productId,
        string productName,
        string productImageUrl,
        decimal unitPrice,
        decimal discount,
        int units)
    {
        return new OrderItem
        {
            Id = OrderItemId.Create(),
            ProductId = productId,
            ProductName = productName,
            ProductImageUrl = productImageUrl,
            UnitPrice = unitPrice,
            Discount = discount,
            Quantity = units
        };
    }

    internal void AddUnits(int units) => Quantity += units;
    public decimal GetTotalPrice() => (UnitPrice * Quantity) - Discount;
}
