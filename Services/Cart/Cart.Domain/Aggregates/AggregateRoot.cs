using Cart.Domain.Events;

namespace Cart.Domain.Aggregates;

/// <summary>
/// Base class for DDD Aggregate Roots with event sourcing support.
/// Enforces invariants, versioning, and uncommitted events.
/// </summary>
public abstract class AggregateRoot
{
    /// <summary>
    /// Unique identifier of the aggregate (e.g., CartId).
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Current version after loading from events (starts at 0 for new).
    /// Used for optimistic concurrency in EventStoreDB.
    /// </summary>
    public long Version { get; private set; } = -1;  // -1 = not loaded

    /// <summary>
    /// Events raised but not yet persisted.
    /// Cleared after successful append to EventStoreDB.
    /// </summary>
    private readonly List<IDomainEvent> _uncommittedEvents = new();

    /// <summary>
    /// Read-only view of uncommitted events (for repository).
    /// </summary>
    public IReadOnlyList<IDomainEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

    /// <summary>
    /// Protected ctor for new aggregates (set Id in derived class).
    /// </summary>
    protected AggregateRoot() { }  // Parameterless for EF/outbox if needed

    /// <summary>
    /// Protected ctor for loading from history (used by repository).
    /// </summary>
    /// <param name="id">Aggregate ID</param>
    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Apply a domain event: mutate state + record as uncommitted.
    /// Called from command methods (e.g., AddItem).
    /// </summary>
    /// <param name="event">The domain event to apply</param>
    protected void Apply(IDomainEvent @event)
    {
        // 1. Mutate state via dynamic dispatch (When method in derived)
        When(@event);

        // 2. Increment version (optimistic concurrency)
        Version++;

        // 3. Record event for persistence
        _uncommittedEvents.Add(@event);
    }

    /// <summary>
    /// Dynamic dispatch to event-specific Apply method in derived class.
    /// Throws if no handler (enforces all events handled).
    /// </summary>
    private void When(IDomainEvent @event)
    {
        var method = GetType().GetMethod(
            nameof(When),
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
            new[] { @event.GetType() });

        if (method == null)
            throw new InvalidOperationException($"No Apply handler for {@event.GetType().Name} in {GetType().Name}");

        method.Invoke(this, new object[] { @event });
    }

    /// <summary>
    /// Load aggregate from event history (replay).
    /// Called by repository on GetAsync.
    /// </summary>
    /// <param name="history">Stream of past events</param>
    public void LoadFromHistory(IEnumerable<IDomainEvent> history)
    {
        if (history == null) throw new ArgumentNullException(nameof(history));

        foreach (var @event in history)
        {
            When(@event);     // Mutate state only (no uncommitted)
            Version++;        // Restore version
        }
    }

    /// <summary>
    /// Clear uncommitted events after successful persistence.
    /// Called by repository after AppendToStream.
    /// </summary>
    public void MarkCommitted()
    {
        _uncommittedEvents.Clear();
    }
}
