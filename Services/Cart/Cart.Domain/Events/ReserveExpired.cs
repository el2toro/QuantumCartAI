using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ReserveExpired(
    Guid CartId,
    SkuId SkuId
) : DomainEventBase;
