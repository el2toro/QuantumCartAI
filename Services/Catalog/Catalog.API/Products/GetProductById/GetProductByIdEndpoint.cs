namespace Catalog.API.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog/products/{productId}", async (Guid productId, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(productId));

            return Results.Ok(result.Product);
        })
       .WithDisplayName("GetProductById")
       .WithDescription("GetProductById")
       .Produces(StatusCodes.Status200OK)
       .WithSummary("Get Product By Id");
    }
}
