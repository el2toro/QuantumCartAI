using CustomerChat.Application.Common;
using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Application.Features.Conversations.DTOs;
using CustomerChat.Domain.Entities;
using CustomerChat.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace CustomerChat.Application.Features.Conversations.Commands;

// ── Command ───────────────────────────────────────────────────────────────────

public record StartConversationCommand(
    Guid CustomerId,
    string Subject,
    string? InitialMessage) : IRequest<Result<ConversationDto>>;

// ── Validator ─────────────────────────────────────────────────────────────────

public sealed class StartConversationCommandValidator : AbstractValidator<StartConversationCommand>
{
    public StartConversationCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");
        RuleFor(x => x.InitialMessage)
            .MaximumLength(4000).When(x => x.InitialMessage is not null)
            .WithMessage("Initial message cannot exceed 4000 characters.");
    }
}

// ── Handler ───────────────────────────────────────────────────────────────────

public sealed class StartConversationCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService,
    IBotResponseService botService) : IRequestHandler<StartConversationCommand, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(
        StartConversationCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = Conversation.Start(
            request.CustomerId,
            request.Subject,
            request.InitialMessage);

        await unitOfWork.Conversations.AddAsync(conversation, cancellationToken);

        // Attempt a bot auto-reply if there's an initial message
        if (!string.IsNullOrWhiteSpace(request.InitialMessage))
        {
            var botReply = await botService.GenerateResponseAsync(
                conversation.Id, request.InitialMessage, cancellationToken);

            if (!string.IsNullOrWhiteSpace(botReply))
                conversation.AddBotMessage(botReply);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Notify agents about a new conversation
        await notificationService.NotifyAgentsAsync(
            "NewConversation",
            new { conversation.Id, conversation.Subject, conversation.CustomerId },
            cancellationToken);

        return Result<ConversationDto>.Success(MapToDto(conversation));
    }

    private static ConversationDto MapToDto(Conversation c) => new(
        c.Id, c.CustomerId, c.AssignedAgentId, c.Subject,
        c.Status.ToString(), c.CreatedAt, c.UpdatedAt, c.ClosedAt,
        c.ClosingReason, c.Messages.Count);
}
