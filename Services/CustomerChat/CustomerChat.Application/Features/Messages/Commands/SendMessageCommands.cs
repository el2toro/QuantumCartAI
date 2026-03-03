using CustomerChat.Application.Common;
using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Application.Features.Conversations.DTOs;
using CustomerChat.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace CustomerChat.Application.Features.Messages.Commands;

// ── Command ───────────────────────────────────────────────────────────────────

public record SendCustomerMessageCommand(
    Guid ConversationId,
    Guid CustomerId,
    string Content) : IRequest<Result<MessageDto>>;

public record SendAgentMessageCommand(
    Guid ConversationId,
    Guid AgentId,
    string Content) : IRequest<Result<MessageDto>>;

// ── Validators ────────────────────────────────────────────────────────────────

public sealed class SendCustomerMessageCommandValidator : AbstractValidator<SendCustomerMessageCommand>
{
    public SendCustomerMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(4000).WithMessage("Message cannot exceed 4000 characters.");
    }
}

public sealed class SendAgentMessageCommandValidator : AbstractValidator<SendAgentMessageCommand>
{
    public SendAgentMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.AgentId).NotEmpty();
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(4000).WithMessage("Message cannot exceed 4000 characters.");
    }
}

// ── Handlers ──────────────────────────────────────────────────────────────────

public sealed class SendCustomerMessageCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService,
    IBotResponseService botService) : IRequestHandler<SendCustomerMessageCommand, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(
        SendCustomerMessageCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations
            .GetByIdWithMessagesAsync(request.ConversationId, cancellationToken);

        if (conversation is null)
            return Result<MessageDto>.Failure($"Conversation '{request.ConversationId}' not found.");

        if (conversation.CustomerId != request.CustomerId)
            return Result<MessageDto>.Failure("You are not a participant of this conversation.");

        conversation.AddCustomerMessage(request.Content);

        // Attempt bot auto-reply if no agent is assigned
        if (conversation.AssignedAgentId is null)
        {
            var botReply = await botService.GenerateResponseAsync(
                conversation.Id, request.Content, cancellationToken);

            if (!string.IsNullOrWhiteSpace(botReply))
                conversation.AddBotMessage(botReply);
        }

        unitOfWork.Conversations.Update(conversation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var sentMessage = conversation.Messages.OrderBy(m => m.CreatedAt).Last();

        await notificationService.NotifyConversationAsync(
            conversation.Id,
            "MessageReceived",
            new { MessageId = sentMessage.Id, request.Content, SenderType = "Customer" },
            cancellationToken);

        return Result<MessageDto>.Success(MapToDto(sentMessage));
    }

    private static MessageDto MapToDto(Domain.Entities.Message m) => new(
        m.Id, m.ConversationId, m.SenderId,
        m.SenderType.ToString(), m.MessageType.ToString(),
        m.Content, m.IsRead, m.ReadAt, m.CreatedAt);
}

public sealed class SendAgentMessageCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService) : IRequestHandler<SendAgentMessageCommand, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(
        SendAgentMessageCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations
            .GetByIdWithMessagesAsync(request.ConversationId, cancellationToken);

        if (conversation is null)
            return Result<MessageDto>.Failure($"Conversation '{request.ConversationId}' not found.");

        conversation.AddAgentMessage(request.AgentId, request.Content);
        unitOfWork.Conversations.Update(conversation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var sentMessage = conversation.Messages.OrderBy(m => m.CreatedAt).Last();

        await notificationService.NotifyConversationAsync(
            conversation.Id,
            "MessageReceived",
            new { MessageId = sentMessage.Id, request.Content, SenderType = "Agent" },
            cancellationToken);

        // Also notify the customer specifically
        await notificationService.NotifyCustomerAsync(
            conversation.CustomerId,
            "AgentReplied",
            new { conversation.Id, request.Content },
            cancellationToken);

        return Result<MessageDto>.Success(MapToDto(sentMessage));
    }

    private static MessageDto MapToDto(Domain.Entities.Message m) => new(
        m.Id, m.ConversationId, m.SenderId,
        m.SenderType.ToString(), m.MessageType.ToString(),
        m.Content, m.IsRead, m.ReadAt, m.CreatedAt);
}
