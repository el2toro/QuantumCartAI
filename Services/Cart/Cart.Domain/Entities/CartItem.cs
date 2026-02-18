using Cart.Domain.ValueObjects;

namespace Cart.Domain.Entities;

public class CartItem
{
    public ProductId ProductId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money DiscountedPrice { get; private set; }

    private CartItem() { }  // EF

    internal CartItem(ProductId productId, Quantity quantity, Money unitPrice, Money discountedPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        DiscountedPrice = discountedPrice;
    }

    internal void ChangeQuantity(Quantity newQty) => Quantity = newQty;
}
