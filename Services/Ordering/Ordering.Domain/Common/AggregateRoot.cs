namespace Ordering.Domain.Common;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<TId> : IEntity<TId>
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    int Version { get; }
}

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    // Domain events for this aggregate
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Version for optimistic concurrency
    public int Version { get; protected set; } = -1; // -1 indicates new aggregate

    // Protected constructor for EF Core
    protected AggregateRoot() : base() { }

    protected AggregateRoot(TId id) : base(id)
    {
    }

    // Add domain event
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
        IncrementVersion();
    }

    // Clear domain events
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Increment version
    public void IncrementVersion()
    {
        Version++;
    }

    // Apply domain event (for event sourcing)
    protected virtual void Apply(IDomainEvent @event)
    {
        // Template method pattern - override in derived classes
        // to handle specific domain events
    }

    // Helper method to get typed domain events
    protected List<TEvent> GetDomainEvents<TEvent>() where TEvent : IDomainEvent
    {
        return _domainEvents.OfType<TEvent>().ToList();
    }
}

// For non-generic ID(using Guid)
public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
{
    protected AggregateRoot() : base() { }

    protected AggregateRoot(Guid id) : base(id)
    {
    }
}

// Event Sourced Aggregate Root implementation
public abstract class EventSourcedAggregateRoot<TId> : AggregateRoot<TId>, IEventSourcedAggregateRoot<TId>
{
    private readonly List<IDomainEvent> _changes = new();

    // Replay events to rebuild state
    public void Apply(IDomainEvent @event)
    {
        When(@event);
        IncrementVersion();
    }

    // Get all uncommitted changes
    public IReadOnlyCollection<IDomainEvent> GetUncommittedChanges() => _changes.AsReadOnly();

    // Mark changes as committed
    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    // Template method for handling specific events
    protected abstract void When(IDomainEvent @event);

    // Raise event (adds to changes and applies it)
    protected void RaiseEvent(IDomainEvent @event)
    {
        _changes.Add(@event);
        Apply(@event);
        AddDomainEvent(@event); // Also add to regular domain events
    }
}

public interface IEventSourcedAggregateRoot<TId> : IAggregateRoot<TId>
{
    void Apply(IDomainEvent @event);
    IReadOnlyCollection<IDomainEvent> GetUncommittedChanges();
    void MarkChangesAsCommitted();
}

// Interface for entities (optional, for completeness)
public interface IEntity<TId>
{
    TId Id { get; }
    bool IsTransient();
}