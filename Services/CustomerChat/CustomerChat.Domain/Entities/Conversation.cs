using CustomerChat.Domain.Enums;
using CustomerChat.Domain.Events;
using CustomerChat.Domain.Exceptions;

namespace CustomerChat.Domain.Entities;

/// <summary>
/// Conversation is the aggregate root. It owns the lifecycle of Messages
/// and enforces all business rules related to a chat session.
/// </summary>
public sealed class Conversation : BaseEntity
{
    private readonly List<Message> _messages = [];

    // Private parameterless ctor for EF Core
    private Conversation() { }

    private Conversation(Guid customerId, string subject, string? initialMessage)
    {
        CustomerId = customerId;
        Subject = subject;
        Status = ConversationStatus.Open;

        if (!string.IsNullOrWhiteSpace(initialMessage))
        {
            var msg = Message.CreateCustomerMessage(Id, customerId, initialMessage);
            _messages.Add(msg);
        }

        RaiseDomainEvent(new ConversationStartedEvent(Id, customerId));
    }

    public Guid CustomerId { get; private set; }
    public Guid? AssignedAgentId { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public ConversationStatus Status { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public string? ClosingReason { get; private set; }

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    // ── Factory ──────────────────────────────────────────────────────────────

    public static Conversation Start(Guid customerId, string subject, string? initialMessage = null)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("CustomerId cannot be empty.");

        if (string.IsNullOrWhiteSpace(subject))
            throw new DomainException("Conversation subject cannot be empty.");

        return new Conversation(customerId, subject, initialMessage);
    }

    // ── Behavior ─────────────────────────────────────────────────────────────

    public void AddCustomerMessage(string content)
    {
        EnsureNotClosed();
        var msg = Message.CreateCustomerMessage(Id, CustomerId, content);
        _messages.Add(msg);
        MarkAsUpdated();
    }

    public void AddAgentMessage(Guid agentId, string content)
    {
        EnsureNotClosed();
        EnsureAgentIsAssigned(agentId);
        var msg = Message.CreateAgentMessage(Id, agentId, content);
        _messages.Add(msg);
        MarkAsUpdated();
    }

    public void AddBotMessage(string content)
    {
        EnsureNotClosed();
        var msg = Message.CreateBotMessage(Id, content);
        _messages.Add(msg);
        MarkAsUpdated();
    }

    public void RequestAgentHandoff()
    {
        if (Status is ConversationStatus.Resolved or ConversationStatus.Closed)
            throw new DomainException("Cannot request agent handoff on a closed conversation.");

        Status = ConversationStatus.PendingAgent;
        AddSystemNotification("Customer requested to speak with a live agent.");
        RaiseDomainEvent(new AgentHandoffRequestedEvent(Id, CustomerId));
        MarkAsUpdated();
    }

    public void AssignAgent(Guid agentId)
    {
        if (agentId == Guid.Empty)
            throw new DomainException("AgentId cannot be empty.");

        if (Status == ConversationStatus.Closed)
            throw new DomainException("Cannot assign agent to a closed conversation.");

        AssignedAgentId = agentId;
        Status = ConversationStatus.AssignedToAgent;
        AddSystemNotification("An agent has joined the conversation.");
        RaiseDomainEvent(new AgentAssignedEvent(Id, agentId, CustomerId));
        MarkAsUpdated();
    }

    public void UnassignAgent()
    {
        AssignedAgentId = null;
        Status = ConversationStatus.PendingAgent;
        AddSystemNotification("Agent has left the conversation.");
        MarkAsUpdated();
    }

    public void Resolve(string reason)
    {
        if (Status == ConversationStatus.Closed)
            throw new DomainException("Conversation is already closed.");

        Status = ConversationStatus.Resolved;
        ClosingReason = reason;
        ClosedAt = DateTime.UtcNow;
        AddSystemNotification($"Conversation resolved: {reason}");
        RaiseDomainEvent(new ConversationResolvedEvent(Id, CustomerId, AssignedAgentId));
        MarkAsUpdated();
    }

    public void Close()
    {
        if (Status == ConversationStatus.Closed)
            throw new DomainException("Conversation is already closed.");

        Status = ConversationStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private void AddSystemNotification(string content)
    {
        var msg = Message.CreateSystemMessage(Id, content);
        _messages.Add(msg);
    }

    private void EnsureNotClosed()
    {
        if (Status is ConversationStatus.Closed or ConversationStatus.Resolved)
            throw new DomainException("Cannot add messages to a closed or resolved conversation.");
    }

    private void EnsureAgentIsAssigned(Guid agentId)
    {
        if (AssignedAgentId != agentId)
            throw new DomainException("Only the assigned agent can send messages to this conversation.");
    }
}
