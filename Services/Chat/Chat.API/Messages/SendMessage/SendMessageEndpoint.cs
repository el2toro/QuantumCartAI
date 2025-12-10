using Carter;
using MediatR;

namespace Chat.API.Messages.SendMessage;

public class SendMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("messages", async (string message, ISender sender) =>
        {
            await sender.Send(new SendMessageCommand(message));
            return Results.Ok();
        });
    }
}
