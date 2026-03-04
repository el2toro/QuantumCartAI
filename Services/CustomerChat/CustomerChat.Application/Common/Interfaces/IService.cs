namespace CustomerChat.Application.Common.Interfaces;

/// <summary>
/// Provides the current user context (customer or agent) from the HTTP request.
/// </summary>
public interface ICurrentUserService
{
    Guid UserId { get; }
    string UserType { get; } // "Customer" | "Agent"
    bool IsAuthenticated { get; }
}

/// <summary>
/// Real-time notification contract.
/// Infrastructure implements this via SignalR.
/// </summary>
public interface INotificationService
{
    Task NotifyConversationAsync(Guid conversationId, string eventName, object payload, CancellationToken ct = default);
    Task NotifyAgentsAsync(string eventName, object payload, CancellationToken ct = default);
    Task NotifyCustomerAsync(Guid customerId, string eventName, object payload, CancellationToken ct = default);
}

/// <summary>
/// Bot/AI response contract. Plug in any LLM here without touching Application logic.
/// </summary>
public interface IBotResponseService
{
    Task<string?> GenerateResponseAsync(Guid conversationId, string userMessage, CancellationToken ct = default);
}

/// <summary>
/// Abstraction for date/time to make code testable.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
