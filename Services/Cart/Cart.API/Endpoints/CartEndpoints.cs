using Cart.Application.Commands;
using Carter;
using Mapster;
using MediatR;

namespace Cart.API.Endpoints;

public class CartEndpoints : ICarterModule
{
    public record AddItemRequest(Guid? CustomerId, Guid ProductId, int Quantity);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("cart/{customerId}", (Guid customerId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok($"Get cart for customer {customerId}");
        });

        app.MapPost("cart", async (AddItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddItemCommand>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithDisplayName("Cart")
        .WithDescription("Cart")
        .WithSummary("Add Item To Cart")
        .Produces(StatusCodes.Status200OK);

        app.MapGet("cart/{cartId}/items", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapPost("cart/{cartId}/checkout", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapPut("cart/{cartId}/items/{productId}/quantity", (int quantity, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapDelete("cart/{cartId}/items/{productId}", (Guid productId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapDelete("cart/{cartId}", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });
    }
}
