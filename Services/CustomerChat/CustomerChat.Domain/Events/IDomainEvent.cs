using MediatR;

namespace CustomerChat.Domain.Entities;

/// <summary>
/// Marker interface for domain events.
/// Extends INotification so MediatR can dispatch them as notifications.
/// </summary>
public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredAt { get; }
}
