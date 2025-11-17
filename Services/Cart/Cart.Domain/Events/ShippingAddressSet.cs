using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ShippingAddressSet(
    Guid CartId,
    Address Address
) : DomainEventBase;
