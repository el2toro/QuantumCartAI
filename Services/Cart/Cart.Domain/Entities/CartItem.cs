using Cart.Domain.ValueObjects;

namespace Cart.Domain.Entities;

public class CartItem
{
    public SkuId SkuId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    private CartItem() { }  // EF

    internal CartItem(SkuId skuId, Quantity quantity, Money unitPrice)
    {
        SkuId = skuId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    internal void ChangeQuantity(Quantity newQty) => Quantity = newQty;
}
