using Carter;
using CustomerChat.Application.Features.Agents.Commands;
using CustomerChat.Application.Features.Conversations.Commands;
using CustomerChat.Application.Features.Conversations.Queries;
using CustomerChat.Application.Features.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerChat.API.Endpoints;

public sealed class ConversationEndpoints : ICarterModule
{
    // ── Request DTOs (API input models, NOT domain objects) ───────────────────────

    public record StartConversationRequest(Guid CustomerId, string Subject, string? InitialMessage);
    public record SendMessageRequest(Guid SenderId, string Content);
    public record HandoffRequest(Guid CustomerId);
    public record AssignAgentRequest(Guid? AgentId);
    public record ResolveRequest(string Reason);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/conversations")
                       .WithTags("Conversations");

        // --- Commands (Write Operations) ---

        group.MapPost("/", async (StartConversationRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new StartConversationCommand(
                request.CustomerId, request.Subject, request.InitialMessage), ct);

            return result.Match(
                dto => Results.CreatedAtRoute("GetConversation", new { id = dto.Id }, dto),
                error => Results.BadRequest(new { error }));
        })
        .WithName("StartConversation")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/{id:guid}/messages/customer", async (Guid id, SendMessageRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new SendCustomerMessageCommand(id, request.SenderId, request.Content), ct);
            return result.Match(Results.Ok, error => Results.BadRequest(new { error }));
        });

        group.MapPost("/{id:guid}/messages/agent", async (Guid id, SendMessageRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new SendAgentMessageCommand(id, request.SenderId, request.Content), ct);
            return result.Match(Results.Ok, error => Results.BadRequest(new { error }));
        });

        //group.MapPost("/{id:guid}/request-handoff", async (Guid id, HandoffRequest request, ISender sender, CancellationToken ct) =>
        //{
        //    var result = await sender.Send(new RequestHandoffCommand(id, request.CustomerId), ct);
        //    return result.Match(_ => Results.Ok(new { message = "Agent handoff requested." }), error => Results.BadRequest(new { error }));
        //});

        group.MapPost("/{id:guid}/assign-agent", async (Guid id, AssignAgentRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new AssignAgentCommand(id, request.AgentId), ct);
            return result.Match(Results.Ok, error => Results.BadRequest(new { error }));
        });

        //group.MapPost("/{id:guid}/resolve", async (Guid id, ResolveRequest request, ISender sender, CancellationToken ct) =>
        //{
        //    var result = await sender.Send(new ResolveConversationCommand(id, request.Reason), ct);
        //    return result.Match(_ => Results.Ok(new { message = "Conversation resolved." }), error => Results.BadRequest(new { error }));
        //});

        // --- Queries (Read Operations) ---

        group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetConversationByIdQuery(id), ct);
            return result.Match(Results.Ok, error => Results.NotFound(new { error }));
        })
        .WithName("GetConversation")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/customer/{customerId:guid}", async (Guid customerId, ISender sender, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default) =>
        {
            var result = await sender.Send(new GetConversationsByCustomerQuery(customerId, page, pageSize), ct);
            return result.Match(Results.Ok, error => Results.BadRequest(new { error }));
        });

        group.MapGet("/pending", async (ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetPendingConversationsQuery(), ct);
            return result.Match(Results.Ok, error => Results.BadRequest(new { error }));
        });
    }
}
