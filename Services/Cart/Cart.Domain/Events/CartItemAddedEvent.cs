using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemAddedEvent(
    Guid CartId,
    ProductId ProductId,
    Quantity Quantity,
    Money UnitPrice
) : DomainEventBase;
