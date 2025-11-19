using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemRemoved(
    Guid CartId,
    ProductId ProductId,
    Quantity Quantity
) : DomainEventBase;