namespace Cart.Domain.Events;

public record PromoRemoved(
    Guid CartId,
    string PromoCode
) : DomainEventBase;