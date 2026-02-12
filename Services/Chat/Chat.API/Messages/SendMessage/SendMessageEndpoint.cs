using Carter;
using MediatR;

namespace Chat.API.Messages.SendMessage;

public record SendMessageRequest(string Message);
public class SendMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("messages", async (SendMessageRequest request, ISender sender) =>
        {
            var result = await sender.Send(new SendMessageCommand(request.Message));
            return Results.Ok(result.Message);
        });
    }
}
