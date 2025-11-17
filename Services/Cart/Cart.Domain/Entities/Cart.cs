using Cart.Domain.Aggregates;
using Cart.Domain.Events;
using Cart.Domain.Exceptions;
using Cart.Domain.Ports;
using Cart.Domain.ValueObjects;

namespace Cart.Domain.Entities;

public class Cart : AggregateRoot
{
    // Private state
    private readonly List<CartItem> _items = new();
    private readonly Dictionary<SkuId, DateTime> _reserves = new();
    private string? _promoCode;
    private Address? _shippingAddress;
    private string? _shippingOption;
    private Money _shippingCost = Money.Zero;

    // Public read-only
    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public string? PromoCode => _promoCode;
    public Address? ShippingAddress => _shippingAddress;
    public string? ShippingOption => _shippingOption;
    public Money ShippingCost => _shippingCost;
    public Money Subtotal => _items.Aggregate(Money.Zero, (sum, i) => sum + i.UnitPrice.Multiply(i.Quantity));

    // New cart
    public Cart(CartId cartId) : base(cartId.Value) { }

    // === COMMAND METHODS (Actions) ===

    public void AddItem(SkuId skuId, Quantity requestedQty, IInventoryQuery inventory, Money unitPrice)
    {
        var atp = inventory.GetAtpAsync(skuId).GetAwaiter().GetResult();  // Sync for aggregate
        var addQty = requestedQty.Min(atp);
        if (addQty == Quantity.Zero) throw new OutOfStockException($"No ATP for {skuId}");

        var existing = _items.FirstOrDefault(i => i.SkuId == skuId);
        if (existing != null)
        {
            Apply(new CartItemQuantityChanged(Id, skuId, existing.Quantity, existing.Quantity + addQty));
        }
        else
        {
            Apply(new CartItemAdded(Id, skuId, addQty, unitPrice));
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(15);
        Apply(new ReserveRequested(Id, skuId, addQty, expiresAt));
    }

    public void RemoveItem(SkuId skuId, Quantity qtyToRemove)
    {
        var item = _items.FirstOrDefault(i => i.SkuId == skuId) ?? throw new ItemNotFoundException();
        var newQty = item.Quantity - qtyToRemove;
        if (newQty.Value < Quantity.Zero.Value) throw new InvalidOperationException();

        if (newQty == Quantity.Zero)
        {
            Apply(new CartItemRemoved(Id, skuId, item.Quantity));
        }
        else
        {
            Apply(new CartItemQuantityChanged(Id, skuId, item.Quantity, newQty));
        }
    }

    public void ApplyPromo(string promoCode, Money discount)
    {
        if (_promoCode == promoCode) return;
        Apply(new PromoApplied(Id, promoCode, discount));
    }

    public void RemovePromo(string promoCode)
    {
        if (_promoCode != promoCode) return;
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

    private void When(CartItemAdded e)
    {
        _items.Add(new CartItem(e.SkuId, e.Quantity, e.UnitPrice));
    }

    private void When(CartItemRemoved e)
    {
        var item = _items.First(i => i.SkuId == e.SkuId);
        _items.Remove(item);
    }

    private void When(CartItemQuantityChanged e)
    {
        var item = _items.First(i => i.SkuId == e.SkuId);
        item.ChangeQuantity(e.NewQuantity);
    }

    private void When(PromoApplied e)
    {
        _promoCode = e.PromoCode;
    }

    private void When(PromoRemoved e)
    {
        _promoCode = null;
    }

    private void When(ShippingAddressSet e)
    {
        _shippingAddress = e.Address;
    }

    private void When(ShippingOptionSelected e)
    {
        _shippingOption = e.OptionId;
        _shippingCost = e.ShippingCost;
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