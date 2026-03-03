namespace CustomerChat.Application.Features.Conversations.DTOs;

public record ConversationDto(
    Guid Id,
    Guid CustomerId,
    Guid? AssignedAgentId,
    string Subject,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ClosedAt,
    string? ClosingReason,
    int MessageCount);

public record ConversationDetailDto(
    Guid Id,
    Guid CustomerId,
    Guid? AssignedAgentId,
    string Subject,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ClosedAt,
    string? ClosingReason,
    IReadOnlyCollection<MessageDto> Messages);

public record MessageDto(
    Guid Id,
    Guid ConversationId,
    Guid? SenderId,
    string SenderType,
    string MessageType,
    string Content,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt);

public record PagedResult<T>(
    IReadOnlyCollection<T> Items,
    int TotalCount,
    int Page,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}
