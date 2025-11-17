using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemQuantityChanged(
    Guid CartId,
    SkuId SkuId,
    Quantity OldQuantity,
    Quantity NewQuantity
) : DomainEventBase;
