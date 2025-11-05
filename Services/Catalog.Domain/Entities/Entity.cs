namespace Catalog.Domain.Entities;

internal class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; } = default!;

    //public IReadOnlyCollection<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();
    //private readonly List<IDomainEvent> _domainEvents = new();

    //protected void AddDomainEvent(IDomainEven domainEven)
    //   => _domainEvents.Add(domainEvent);

    //public void ClearDomainEvents => _domainEvents.Clear();

    //protected void Rise(IDomainEvent domainEvent)
    //    => AddDomainEvent(domainEvent);
    public bool Equals(Entity<TId>? other)
         => other is not null && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override bool Equals(object? obj)
        => obj is Entity<TId> other && Equals(other);

    public override int GetHashCode() => EqualityComparer<TId>.Default.GetHashCode(Id);

    public static bool operator ==(Entity<TId>? entityLeft, Entity<TId>? entityRight)
        => Equals(entityLeft, entityRight);

    public static bool operator !=(Entity<TId>? entityLeft, Entity<TId>? entityRight)
        => !Equals(entityLeft, entityRight);
}
