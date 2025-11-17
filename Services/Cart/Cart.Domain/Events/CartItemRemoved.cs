using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemRemoved(
    Guid CartId,
    SkuId SkuId,
    Quantity Quantity
) : DomainEventBase;