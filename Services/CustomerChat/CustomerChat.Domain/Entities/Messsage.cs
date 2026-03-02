using CustomerChat.Domain.Enums;
using CustomerChat.Domain.Exceptions;

namespace CustomerChat.Domain.Entities;

/// <summary>
/// Message belongs to a Conversation. It is NOT an aggregate root —
/// it is always accessed through its parent Conversation.
/// </summary>
public sealed class Message : BaseEntity
{
    private Message() { }

    private Message(Guid conversationId, Guid? senderId, SenderType senderType,
        MessageType messageType, string content)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        SenderType = senderType;
        MessageType = messageType;
        Content = content;
        IsRead = false;
    }

    public Guid ConversationId { get; private set; }
    public Guid? SenderId { get; private set; }
    public SenderType SenderType { get; private set; }
    public MessageType MessageType { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public bool IsRead { get; private set; }
    public DateTime? ReadAt { get; private set; }

    // Navigation for EF Core
    public Conversation? Conversation { get; private set; }

    // ── Factories ─────────────────────────────────────────────────────────────

    public static Message CreateCustomerMessage(Guid conversationId, Guid customerId, string content)
    {
        ValidateContent(content);
        return new Message(conversationId, customerId, SenderType.Customer, MessageType.Text, content);
    }

    public static Message CreateAgentMessage(Guid conversationId, Guid agentId, string content)
    {
        ValidateContent(content);
        return new Message(conversationId, agentId, SenderType.Agent, MessageType.Text, content);
    }

    public static Message CreateBotMessage(Guid conversationId, string content)
    {
        ValidateContent(content);
        return new Message(conversationId, null, SenderType.Bot, MessageType.Text, content);
    }

    public static Message CreateSystemMessage(Guid conversationId, string content) =>
        new(conversationId, null, SenderType.System, MessageType.SystemNotification, content);

    // ── Behavior ─────────────────────────────────────────────────────────────

    public void MarkAsRead()
    {
        if (IsRead) return;
        IsRead = true;
        ReadAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Message content cannot be empty.");

        if (content.Length > 4000)
            throw new DomainException("Message content exceeds maximum allowed length of 4000 characters.");
    }
}
