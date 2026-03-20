namespace BuildingBlocks.Messaging.IntegrationEvents.Order;

public record OrderCreatedEvent(Guid OrderId, Guid CustomerId, string OrderNumber) : BaseEvent;

