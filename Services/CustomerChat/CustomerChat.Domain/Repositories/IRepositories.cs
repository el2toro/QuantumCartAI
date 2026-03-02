using CustomerChat.Domain.Entities;

namespace CustomerChat.Domain.Repositories;

public interface IConversationRepository
{
    Task<Conversation?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Conversation?> GetByIdWithMessagesAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Conversation>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default);
    Task<IEnumerable<Conversation>> GetPendingAgentConversationsAsync(CancellationToken ct = default);
    Task AddAsync(Conversation conversation, CancellationToken ct = default);
    void Update(Conversation conversation);
}

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Message>> GetByConversationIdAsync(Guid conversationId, int skip, int take, CancellationToken ct = default);
    Task<int> GetUnreadCountAsync(Guid conversationId, CancellationToken ct = default);
}

public interface IAgentRepository
{
    Task<Agent?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Agent?> GetAvailableAgentAsync(CancellationToken ct = default);
    Task<IEnumerable<Agent>> GetAllOnlineAsync(CancellationToken ct = default);
    Task AddAsync(Agent agent, CancellationToken ct = default);
    void Update(Agent agent);
}

public interface IUnitOfWork
{
    IConversationRepository Conversations { get; }
    IMessageRepository Messages { get; }
    IAgentRepository Agents { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
