namespace Catalog.API.Products.GetProductsById;

public class GetProductsByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog/products", async (Guid[] productIds, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByIdQuery(productIds));

            return Results.Ok(result.Products);
        })
       .WithDisplayName("GetProductsById")
       .WithDescription("GetProductsById")
       .Produces(StatusCodes.Status200OK)
       .WithSummary("Get Products By Id");
    }
}
