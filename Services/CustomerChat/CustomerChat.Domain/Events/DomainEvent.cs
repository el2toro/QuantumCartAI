using CustomerChat.Domain.Entities;

namespace CustomerChat.Domain.Events;

public abstract record BaseDomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}

public record ConversationStartedEvent(
    Guid ConversationId,
    Guid CustomerId) : BaseDomainEvent;

public record MessageSentEvent(
    Guid ConversationId,
    Guid MessageId,
    string SenderType) : BaseDomainEvent;

public record AgentHandoffRequestedEvent(
    Guid ConversationId,
    Guid CustomerId) : BaseDomainEvent;

public record AgentAssignedEvent(
    Guid ConversationId,
    Guid AgentId,
    Guid CustomerId) : BaseDomainEvent;

public record ConversationResolvedEvent(
    Guid ConversationId,
    Guid CustomerId,
    Guid? AgentId) : BaseDomainEvent;
