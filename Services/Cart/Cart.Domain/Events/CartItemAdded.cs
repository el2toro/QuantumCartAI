using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemAdded(
    Guid CartId,
    SkuId SkuId,
    Quantity Quantity,
    Money UnitPrice
) : DomainEventBase;
