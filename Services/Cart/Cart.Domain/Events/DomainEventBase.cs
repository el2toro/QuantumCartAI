using System.Text.Json.Serialization;

namespace Cart.Domain.Events;

/// <summary>
/// Abstract base record for all domain events.
/// Provides default implementation of IDomainEvent:
/// - Auto EventType (from derived class name)
/// - Auto OccurredOn (UTC now)
/// - Optional Metadata dictionary
/// </summary>
public abstract record DomainEventBase : IDomainEvent
{
    /// <summary>
    /// Timestamp when the event occurred (UTC).
    /// Set at creation time – immutable.
    /// </summary>
    [JsonPropertyName("occurredOn")]
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Optional metadata: correlationId, causationId, userId, abTestVariant, etc.
    /// Populated by infrastructure (MediatR pipeline, API gateway).
    /// </summary>
    [JsonPropertyName("metadata")]
    public IReadOnlyDictionary<string, string>? Metadata { get; init; }

    /// <summary>
    /// Event type derived from concrete class name.
    /// Used by EventStoreDB, Kafka, projections.
    /// </summary>
    [JsonIgnore]  // Not serialized – reconstructed at runtime
    public string EventType => GetType().Name;
}