using CustomerChat.Domain.Enums;
using CustomerChat.Domain.Exceptions;

namespace CustomerChat.Domain.Entities;

// <summary>
/// Represents a support agent who can be assigned to conversations.
/// </summary>
public sealed class Agent : BaseEntity
{
    private Agent() { }

    private Agent(string name, string email)
    {
        Name = name;
        Email = email;
        Status = AgentStatus.Offline;
        ActiveConversationCount = 0;
    }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public AgentStatus Status { get; private set; }
    public int ActiveConversationCount { get; private set; }
    public int MaxConcurrentConversations { get; private set; } = 5;
    public DateTime? LastSeenAt { get; private set; }

    public static Agent Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Agent name cannot be empty.");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Agent email cannot be empty.");

        return new Agent(name, email);
    }

    public bool IsAvailable =>
        Status == AgentStatus.Online && ActiveConversationCount < MaxConcurrentConversations;

    public void SetStatus(AgentStatus status)
    {
        Status = status;
        LastSeenAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    public void IncrementConversationCount()
    {
        if (ActiveConversationCount >= MaxConcurrentConversations)
            throw new DomainException("Agent has reached the maximum number of concurrent conversations.");

        ActiveConversationCount++;
        MarkAsUpdated();
    }

    public void DecrementConversationCount()
    {
        if (ActiveConversationCount > 0)
        {
            ActiveConversationCount--;
            MarkAsUpdated();
        }
    }

    public void UpdateMaxConcurrentConversations(int max)
    {
        if (max < 1)
            throw new DomainException("MaxConcurrentConversations must be at least 1.");
        MaxConcurrentConversations = max;
        MarkAsUpdated();
    }
}
