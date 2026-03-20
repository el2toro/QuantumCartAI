namespace BuildingBlocks.Messaging.IntegrationEvents.Order;

public record BaseEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
