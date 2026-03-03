using CustomerChat.Application.Common;
using CustomerChat.Application.Features.Conversations.DTOs;
using CustomerChat.Domain.Repositories;
using MediatR;

namespace CustomerChat.Application.Features.Conversations.Queries;

// ── Queries ───────────────────────────────────────────────────────────────────

public record GetConversationByIdQuery(Guid ConversationId) : IRequest<Result<ConversationDetailDto>>;

public record GetConversationsByCustomerQuery(
    Guid CustomerId,
    int Page = 1,
    int PageSize = 20) : IRequest<Result<PagedResult<ConversationDto>>>;

public record GetPendingConversationsQuery() : IRequest<Result<IEnumerable<ConversationDto>>>;

// ── Handlers ──────────────────────────────────────────────────────────────────

public sealed class GetConversationByIdQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetConversationByIdQuery, Result<ConversationDetailDto>>
{
    public async Task<Result<ConversationDetailDto>> Handle(
        GetConversationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations
            .GetByIdWithMessagesAsync(request.ConversationId, cancellationToken);

        if (conversation is null)
            return Result<ConversationDetailDto>.Failure("Conversation not found.");

        var dto = new ConversationDetailDto(
            conversation.Id,
            conversation.CustomerId,
            conversation.AssignedAgentId,
            conversation.Subject,
            conversation.Status.ToString(),
            conversation.CreatedAt,
            conversation.UpdatedAt,
            conversation.ClosedAt,
            conversation.ClosingReason,
            conversation.Messages
                .OrderBy(m => m.CreatedAt)
                .Select(m => new MessageDto(
                    m.Id, m.ConversationId, m.SenderId,
                    m.SenderType.ToString(), m.MessageType.ToString(),
                    m.Content, m.IsRead, m.ReadAt, m.CreatedAt))
                .ToList());

        return Result<ConversationDetailDto>.Success(dto);
    }
}

public sealed class GetConversationsByCustomerQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetConversationsByCustomerQuery, Result<PagedResult<ConversationDto>>>
{
    public async Task<Result<PagedResult<ConversationDto>>> Handle(
        GetConversationsByCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var conversations = await unitOfWork.Conversations
            .GetByCustomerIdAsync(request.CustomerId, cancellationToken);

        var list = conversations.ToList();
        var total = list.Count;
        var paged = list
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new ConversationDto(
                c.Id, c.CustomerId, c.AssignedAgentId, c.Subject,
                c.Status.ToString(), c.CreatedAt, c.UpdatedAt, c.ClosedAt,
                c.ClosingReason, c.Messages.Count))
            .ToList();

        return Result<PagedResult<ConversationDto>>.Success(
            new PagedResult<ConversationDto>(paged, total, request.Page, request.PageSize));
    }
}

public sealed class GetPendingConversationsQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetPendingConversationsQuery, Result<IEnumerable<ConversationDto>>>
{
    public async Task<Result<IEnumerable<ConversationDto>>> Handle(
        GetPendingConversationsQuery request,
        CancellationToken cancellationToken)
    {
        var conversations = await unitOfWork.Conversations
            .GetPendingAgentConversationsAsync(cancellationToken);

        var dtos = conversations.Select(c => new ConversationDto(
            c.Id, c.CustomerId, c.AssignedAgentId, c.Subject,
            c.Status.ToString(), c.CreatedAt, c.UpdatedAt, c.ClosedAt,
            c.ClosingReason, c.Messages.Count));

        return Result<IEnumerable<ConversationDto>>.Success(dtos);
    }
}
