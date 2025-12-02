using Carter;
using MediatR;
using Ordering.Application.Handlers.Commands;

namespace Ordering.AAPI.Endpoints;

public class OrderingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (ISender sender) =>
        {
            var result = await sender.Send(new CreateOrderCommand());
            return Results.Created();
        });
    }
}
