using Microsoft.AspNetCore.SignalR;

namespace CustomerChat.Infrastructure.Hubs;

/// <summary>
/// SignalR hub for real-time bi-directional chat communication.
/// Clients join conversation groups so only relevant parties receive events.
/// </summary>

//[Authorize]
public sealed class ChatHub : Hub
{
    public const string HubUrl = "/hubs/chat";

    // Group naming conventions
    public static string ConversationGroup(Guid conversationId) => $"conversation:{conversationId}";
    public static string AgentsGroup => "agents";
    public static string CustomerGroup(Guid customerId) => $"customer:{customerId}";

    /// <summary>
    /// Client calls this to subscribe to a conversation's real-time events.
    /// </summary>
    public async Task JoinConversation(Guid conversationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, ConversationGroup(conversationId));
    }

    public async Task LeaveConversation(Guid conversationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConversationGroup(conversationId));
    }

    /// <summary>
    /// Agent calls this to subscribe to all incoming conversation notifications.
    /// </summary>
    public async Task JoinAgentsGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, AgentsGroup);
    }

    public override async Task OnConnectedAsync()
    {
        // If user is a customer, subscribe them to their personal notifications
        var userId = Context.UserIdentifier;
        if (userId is not null)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"customer:{userId}");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // SignalR handles group cleanup automatically on disconnect
        await base.OnDisconnectedAsync(exception);
    }
}
