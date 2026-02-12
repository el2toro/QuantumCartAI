namespace SharedKernel.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    private int? _requestedHashCode;

    // Primary identifier
    public virtual TId Id { get; protected set; }

    // Domain Events
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Check if entity is transient (not persisted)
    public virtual bool IsTransient()
    {
        return EqualityComparer<TId>.Default.Equals(Id, default!);
    }

    // Default constructor for EF Core
    protected Entity()
    {
        Id = default!;
    }

    // Constructor with ID
    protected Entity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    // Add domain event
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Remove domain event
    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    // Clear all domain events
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Equality implementation
    public bool Equals(Entity<TId> other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (GetType() != other.GetType())
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity<TId>);
    }

    public override int GetHashCode()
    {
        if (IsTransient())
        {
            return base.GetHashCode();
        }

        if (!_requestedHashCode.HasValue)
        {
            _requestedHashCode = Id.GetHashCode() ^ 31;
        }

        return _requestedHashCode.Value;
    }

    // Operator overloads
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    // ToString override
    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    // Clone helper
    protected virtual Entity<TId> CreateClone()
    {
        return (Entity<TId>)MemberwiseClone();
    }
}
