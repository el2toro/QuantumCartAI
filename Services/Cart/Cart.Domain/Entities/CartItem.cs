using Cart.Domain.ValueObjects;

namespace Cart.Domain.Entities;

public class CartItem
{
    public ProductId ProductId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    private CartItem() { }  // EF

    internal CartItem(ProductId productId, Quantity quantity, Money unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    internal void ChangeQuantity(Quantity newQty) => Quantity = newQty;
}
