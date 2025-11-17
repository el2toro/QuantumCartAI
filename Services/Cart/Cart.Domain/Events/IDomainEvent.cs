using System.Text.Json.Serialization;

namespace Cart.Domain.Events;

/// <summary>
/// Marker interface for all domain events in the system.
/// Ensures type safety in AggregateRoot.Apply/When and event store serialization.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Unique event type identifier (e.g., "CartItemAdded").
    /// Used as EventStoreDB event type and for routing/projections.
    /// </summary>
    [JsonIgnore]  // Not serialized – derived from class name
    string EventType => GetType().Name;

    /// <summary>
    /// Timestamp when the event was created (UTC).
    /// Used for ordering, auditing, and AI training timelines.
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// Optional metadata (e.g., UserId, CorrelationId, CausationId).
    /// Populated by infrastructure (e.g., MediatR pipeline).
    /// </summary>
    IReadOnlyDictionary<string, string>? Metadata { get; }
}