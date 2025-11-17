namespace Cart.Domain.Events;

public record CartAbandoned(
    Guid CartId
) : DomainEventBase;
