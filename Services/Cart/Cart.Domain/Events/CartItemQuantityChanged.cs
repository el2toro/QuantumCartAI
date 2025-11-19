using Cart.Domain.ValueObjects;

namespace Cart.Domain.Events;

public record CartItemQuantityChanged(
    Guid CartId,
    ProductId ProductId,
    Quantity OldQuantity,
    Quantity NewQuantity
) : DomainEventBase;
