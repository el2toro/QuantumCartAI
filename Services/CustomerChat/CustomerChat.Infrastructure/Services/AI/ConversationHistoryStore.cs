using Microsoft.SemanticKernel.ChatCompletion;

namespace CustomerChat.Infrastructure.Services.AI;

/// <summary>
/// Maintains per-conversation chat history so the AI remembers
/// what was said earlier in the same session (multi-turn memory).
/// Uses an in-memory dictionary — swap for Redis in production
/// to survive app restarts and scale across multiple instances.
/// </summary>
public sealed class ConversationHistoryStore
{
    private readonly Dictionary<Guid, ChatHistory> _histories = new();
    private const int MaxMessagesPerConversation = 20; // Keeps context window lean

    public ChatHistory GetOrCreate(Guid conversationId, string systemPrompt)
    {
        if (_histories.TryGetValue(conversationId, out var existing))
            return existing;

        var history = new ChatHistory(systemPrompt);
        _histories[conversationId] = history;
        return history;
    }

    public void AddUserMessage(Guid conversationId, string message)
    {
        if (_histories.TryGetValue(conversationId, out var history))
        {
            history.AddUserMessage(message);
            TrimIfNeeded(history);
        }
    }

    public void AddAssistantMessage(Guid conversationId, string message)
    {
        if (_histories.TryGetValue(conversationId, out var history))
            history.AddAssistantMessage(message);
    }

    public void Remove(Guid conversationId) =>
        _histories.Remove(conversationId);

    /// <summary>
    /// Keep the last N messages to avoid exceeding the context window and
    /// to control token costs. Always preserves the system prompt (index 0).
    /// </summary>
    private static void TrimIfNeeded(ChatHistory history)
    {
        // Index 0 is always the system prompt — never remove it
        while (history.Count > MaxMessagesPerConversation + 1)
            history.RemoveAt(1);
    }
}
