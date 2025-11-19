using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemAdded(
    Guid CartId,
    ProductId ProductId,
    Quantity Quantity,
    Money UnitPrice
) : DomainEventBase;
