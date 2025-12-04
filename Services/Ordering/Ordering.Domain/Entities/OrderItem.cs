using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItemId Id { get; private set; }
    public ProductId ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductImageUrl { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public int Units { get; private set; }

    private OrderItem() { }

    public static OrderItem Create(
        ProductId productId,
        string productName,
        string productImageUrl,
        decimal unitPrice,
        decimal discount,
        int units = 1)
    {
        // if (units <= 0)
        //  throw new OrderingDomainException("Invalid number of units");

        // if (unitPrice <= 0)
        //  throw new OrderingDomainException("Invalid unit price");

        return new OrderItem
        {
            Id = OrderItemId.From(Guid.NewGuid().ToString()),
            ProductId = productId,
            ProductName = productName,
            ProductImageUrl = productImageUrl,
            UnitPrice = unitPrice,
            Discount = discount,
            Units = units
        };
    }

    public void AddUnits(int units)
    {
        if (units < 0)
            //   throw new OrderingDomainException("Invalid units");

            Units += units;
    }

    public void SetNewQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            //    throw new OrderingDomainException("Invalid quantity");

            Units = newQuantity;
    }

    public decimal GetTotalPrice()
    {
        return (UnitPrice * Units) - Discount;
    }
}