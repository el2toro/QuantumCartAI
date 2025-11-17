using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record ShippingOptionSelected(
    Guid CartId,
    string OptionId,
    Money ShippingCost
) : DomainEventBase;
