using Cart.Domain.Aggregates;
using Cart.Domain.Events;
using Cart.Domain.Exceptions;
using Cart.Domain.ValueObjects;

namespace Cart.Domain.Entities;

public class Cart : AggregateRoot
{
    // Private state
    private readonly List<CartItem> _items = new();
    private readonly Dictionary<SkuId, DateTime> _reserves = new();

    // Public read-only
    public CustomerId CustomerId { get; private set; }
    public CartStatus Status { get; private set; } = CartStatus.Active;
    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public string? PromoCode { get; private set; }
    public Address? ShippingAddress { get; private set; }
    public string? ShippingOption { get; private set; }
    public Money ShippingCost { get; private set; }
    public Money Subtotal => _items.Aggregate(Money.Zero(Currency), (sum, i) => sum + i.UnitPrice.Multiply(i.Quantity));
    public Currency Currency { get; private set; }

    // New cart
    public Cart(CartId cartId, Currency currency) : base(cartId.Value)
    {
        Currency = currency;
        ShippingCost = Money.Zero(currency);
    }

    public void AddItem(ProductId productId, Quantity requestedQty, Money unitPrice)
    {
        // var atp = inventory.GetAtpAsync(skuId).GetAwaiter().GetResult();  // Sync for aggregate
        //var addQty = requestedQty.Min(Quantity.From(1));
        //if (addQty == Quantity.Zero) throw new OutOfStockException($"No ATP for {skuId}");


        // Check if product exists in Catalog/Inventory (call CatalogService)
        // Then check quantity available of that product
        // If product quantity is 0, ProductOutOfStock

        // CHeck discount rules if any (call DiscountService)
        // Then update price if any discount applied

        var existing = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existing is not null)
        {
            Apply(new CartItemQuantityChanged(Id, productId, existing.Quantity, existing.Quantity + requestedQty));
        }
        else
        {
            Apply(new CartItemAddedEvent(Id, productId, requestedQty, unitPrice));
        }

        //var expiresAt = DateTime.UtcNow.AddMinutes(15);
        //Apply(new ReserveRequested(Id, skuId, addQty, expiresAt));
    }

    public void RemoveItem(ProductId productId, Quantity qtyToRemove)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId) ?? throw new ItemNotFoundException();
        var newQty = item.Quantity - qtyToRemove;
        if (newQty.Value < Quantity.Zero.Value) throw new InvalidOperationException();

        if (newQty == Quantity.Zero)
        {
            Apply(new CartItemRemoved(Id, productId, item.Quantity));
        }
        else
        {
            Apply(new CartItemQuantityChanged(Id, productId, item.Quantity, newQty));
        }
    }

    public void ApplyPromo(string promoCode, Money discount)
    {
        if (PromoCode == promoCode) return;
        Apply(new PromoApplied(Id, promoCode, discount));
    }

    public void RemovePromo(string promoCode)
    {
        if (PromoCode != promoCode) return;
        Apply(new PromoRemoved(Id, promoCode));
    }

    public void SetShippingAddress(Address address)
    {
        address.Validate();
        Apply(new ShippingAddressSet(Id, address));
    }

    public void SelectShippingOption(string optionId, Money cost)
    {
        Apply(new ShippingOptionSelected(Id, optionId, cost));

    }


    // === EVENT HANDLERS (When) ===

    private void When(CartItemAddedEvent e)
    {
        _items.Add(new CartItem(e.ProductId, e.Quantity, e.UnitPrice));
    }

    private void When(CartItemRemoved e)
    {
        var item = _items.First(i => i.ProductId == e.ProductId);
        _items.Remove(item);
    }

    private void When(CartItemQuantityChanged e)
    {
        var item = _items.First(i => i.ProductId == e.ProductId);
        item.ChangeQuantity(e.NewQuantity);
    }

    private void When(PromoApplied e)
    {
        PromoCode = e.PromoCode;
    }

    private void When(PromoRemoved e)
    {
        PromoCode = null;
    }

    private void When(ShippingAddressSet e)
    {
        ShippingAddress = e.Address;
    }

    private void When(ShippingOptionSelected e)
    {
        ShippingOption = e.OptionId;
        ShippingCost = e.ShippingCost;
    }

    private void When(ReserveRequested e)
    {
        _reserves[e.SkuId] = e.ExpiresAt;
    }

    private void When(ReserveSucceeded e) { }
    private void When(ReserveFailed e) { }
    private void When(ReserveExpired e)
    {
        _reserves.Remove(e.SkuId);
        // Trigger removal if needed
    }

    private void When(CartAbandoned e) { }
}