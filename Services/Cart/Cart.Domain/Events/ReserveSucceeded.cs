using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ReserveSucceeded(
    Guid CartId,
    SkuId SkuId
) : DomainEventBase;