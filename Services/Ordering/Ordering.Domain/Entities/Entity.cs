using Ordering.Domain.Events;

namespace Ordering.Domain.Entities;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    // Base identifier for all entities
    private int? _requestedHashCode;

    // For value objects comparison and equality
    public virtual Guid Id { get; protected set; }

    // Domain Events for this entity
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Add domain event
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Remove domain event
    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    // Clear all domain events
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Check if entity is transient (not persisted)
    public bool IsTransient()
    {
        return Id == default || Id == Guid.Empty;
    }

    // Equality comparison
    public override bool Equals(object obj)
    {
        if (obj is not Entity entity)
            return false;

        if (ReferenceEquals(this, entity))
            return true;

        if (GetType() != entity.GetType())
            return false;

        if (entity.IsTransient() || IsTransient())
            return false;

        return entity.Id == Id;
    }
    // Get hash code
    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution

            return _requestedHashCode.Value;
        }
        else
        {
            return base.GetHashCode();
        }
    }
    // Operator overloads for equality
    public static bool operator ==(Entity left, Entity right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}

