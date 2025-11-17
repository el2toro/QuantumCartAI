using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ReserveFailed(
    Guid CartId,
    SkuId SkuId,
    string Reason
) : DomainEventBase;
