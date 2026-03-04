using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CustomerChat.Infrastructure.Services;

/// <summary>
/// Implements INotificationService using SignalR.
/// The Application layer only depends on the interface — swapping SignalR for WebSockets
/// or SSE only requires changing this class.
/// </summary>
public sealed class SignalRNotificationService(
    IHubContext<ChatHub> hubContext) : INotificationService
{
    public async Task NotifyConversationAsync(Guid conversationId, string eventName, object payload,
        CancellationToken ct = default)
    {
        await hubContext.Clients
            .Group(ChatHub.ConversationGroup(conversationId))
            .SendAsync(eventName, payload, ct);
    }

    public async Task NotifyAgentsAsync(string eventName, object payload, CancellationToken ct = default)
    {
        await hubContext.Clients
            .Group(ChatHub.AgentsGroup)
            .SendAsync(eventName, payload, ct);
    }

    public async Task NotifyCustomerAsync(Guid customerId, string eventName, object payload,
        CancellationToken ct = default)
    {
        await hubContext.Clients
            .Group(ChatHub.CustomerGroup(customerId))
            .SendAsync(eventName, payload, ct);
    }
}
