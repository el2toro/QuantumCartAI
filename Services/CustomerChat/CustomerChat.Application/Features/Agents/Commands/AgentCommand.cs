using CustomerChat.Application.Common;
using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Application.Features.Conversations.DTOs;
using CustomerChat.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace CustomerChat.Application.Features.Agents.Commands;

public record AssignAgentCommand(
    Guid ConversationId,
    Guid? PreferredAgentId = null) : IRequest<Result<ConversationDto>>;

public record RequestHandoffCommand(
    Guid ConversationId,
    Guid CustomerId) : IRequest<Result>;

public record ResolveConversationCommand(
    Guid ConversationId,
    string Reason) : IRequest<Result>;

public sealed class AssignAgentCommandValidator : AbstractValidator<AssignAgentCommand>
{
    public AssignAgentCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
    }
}

public sealed class RequestHandoffCommandValidator : AbstractValidator<RequestHandoffCommand>
{
    public RequestHandoffCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

public sealed class AssignAgentCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService) : IRequestHandler<AssignAgentCommand, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(AssignAgentCommand request, CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations.GetByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result<ConversationDto>.Failure("Conversation not found.");

        Domain.Entities.Agent? agent;
        if (request.PreferredAgentId.HasValue)
        {
            agent = await unitOfWork.Agents.GetByIdAsync(request.PreferredAgentId.Value, cancellationToken);
            if (agent is null || !agent.IsAvailable)
                return Result<ConversationDto>.Failure("Preferred agent is not available.");
        }
        else
        {
            agent = await unitOfWork.Agents.GetAvailableAgentAsync(cancellationToken);
            if (agent is null)
                return Result<ConversationDto>.Failure("No agents are currently available.");
        }

        conversation.AssignAgent(agent.Id);
        agent.IncrementConversationCount();

        unitOfWork.Conversations.Update(conversation);
        unitOfWork.Agents.Update(agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyConversationAsync(
            conversation.Id, "AgentAssigned",
            new { AgentId = agent.Id, AgentName = agent.Name }, cancellationToken);

        return Result<ConversationDto>.Success(new ConversationDto(
            conversation.Id, conversation.CustomerId, conversation.AssignedAgentId,
            conversation.Subject, conversation.Status.ToString(),
            conversation.CreatedAt, conversation.UpdatedAt, conversation.ClosedAt,
            conversation.ClosingReason, conversation.Messages.Count));
    }
}

public sealed class RequestHandoffCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService) : IRequestHandler<RequestHandoffCommand, Result>
{
    public async Task<Result> Handle(RequestHandoffCommand request, CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations.GetByIdWithMessagesAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure("Conversation not found.");

        if (conversation.CustomerId != request.CustomerId)
            return Result.Failure("Unauthorized.");

        conversation.RequestAgentHandoff();
        unitOfWork.Conversations.Update(conversation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAgentsAsync("HandoffRequested",
            new { conversation.Id, conversation.CustomerId, conversation.Subject }, cancellationToken);

        return Result.Success();
    }
}

public sealed class ResolveConversationCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationService notificationService) : IRequestHandler<ResolveConversationCommand, Result>
{
    public async Task<Result> Handle(ResolveConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await unitOfWork.Conversations.GetByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure("Conversation not found.");

        conversation.Resolve(request.Reason);

        if (conversation.AssignedAgentId.HasValue)
        {
            var agent = await unitOfWork.Agents.GetByIdAsync(conversation.AssignedAgentId.Value, cancellationToken);
            agent?.DecrementConversationCount();
            if (agent is not null) unitOfWork.Agents.Update(agent);
        }

        unitOfWork.Conversations.Update(conversation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyConversationAsync(
            conversation.Id, "ConversationResolved",
            new { conversation.Id, request.Reason }, cancellationToken);

        return Result.Success();
    }
}
