using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ReserveRequested(
    Guid CartId,
    SkuId SkuId,
    Quantity Quantity,
    DateTime ExpiresAt
) : DomainEventBase;