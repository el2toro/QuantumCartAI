using CustomerChat.Domain.Entities;
using CustomerChat.Domain.Enums;
using CustomerChat.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerChat.Infrastructure.Persistence.Repositories;

public sealed class ConversationRepository(ChatDbContext context) : IConversationRepository
{
    public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Conversations.FindAsync([id], ct);

    public async Task<Conversation?> GetByIdWithMessagesAsync(Guid id, CancellationToken ct = default) =>
        await context.Conversations
            .Include(c => c.Messages)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IEnumerable<Conversation>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default) =>
        await context.Conversations
            .Where(c => c.CustomerId == customerId)
            .OrderByDescending(c => c.UpdatedAt ?? c.CreatedAt)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<IEnumerable<Conversation>> GetPendingAgentConversationsAsync(CancellationToken ct = default) =>
        await context.Conversations
            .Where(c => c.Status == ConversationStatus.PendingAgent)
            .OrderBy(c => c.UpdatedAt ?? c.CreatedAt)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task AddAsync(Conversation conversation, CancellationToken ct = default) =>
        await context.Conversations.AddAsync(conversation, ct);

    public void Update(Conversation conversation)
    {
        var entry = context.Entry(conversation);
        if (entry.State == EntityState.Detached)
            context.Conversations.Attach(conversation).State = EntityState.Modified;
        // Already tracked → EF change tracker handles it automatically. No call needed.
    }
}

public sealed class MessageRepository(ChatDbContext context) : IMessageRepository
{
    public async Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Messages.FindAsync([id], ct);

    public async Task<IEnumerable<Message>> GetByConversationIdAsync(
        Guid conversationId, int skip, int take, CancellationToken ct = default) =>
        await context.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderByDescending(m => m.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);

    public async Task<int> GetUnreadCountAsync(Guid conversationId, CancellationToken ct = default) =>
        await context.Messages
            .CountAsync(m => m.ConversationId == conversationId && !m.IsRead, ct);
}

public sealed class AgentRepository(ChatDbContext context) : IAgentRepository
{
    public async Task<Agent?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Agents.FindAsync([id], ct);

    public async Task<Agent?> GetAvailableAgentAsync(CancellationToken ct = default) =>
        await context.Agents
            .Where(a => a.Status == AgentStatus.Online)
            .OrderBy(a => a.ActiveConversationCount) // Round-robin: least-loaded first
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<Agent>> GetAllOnlineAsync(CancellationToken ct = default) =>
        await context.Agents
            .Where(a => a.Status == AgentStatus.Online)
            .ToListAsync(ct);

    public async Task AddAsync(Agent agent, CancellationToken ct = default) =>
        await context.Agents.AddAsync(agent, ct);

    public void Update(Agent agent) =>
        context.Agents.Update(agent);
}

/// <summary>
/// Unit of Work pattern: wraps a single DbContext transaction.
/// Ensures all writes within a use-case are committed atomically.
/// </summary>
public sealed class UnitOfWork(
    ChatDbContext context,
    IConversationRepository conversations,
    IMessageRepository messages,
    IAgentRepository agents) : IUnitOfWork
{
    public IConversationRepository Conversations => conversations;
    public IMessageRepository Messages => messages;
    public IAgentRepository Agents => agents;

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        context.SaveChangesAsync(ct);
}
