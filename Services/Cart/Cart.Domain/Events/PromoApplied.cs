using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record PromoApplied(
    Guid CartId,
    string PromoCode,
    Money Discount
) : DomainEventBase;
