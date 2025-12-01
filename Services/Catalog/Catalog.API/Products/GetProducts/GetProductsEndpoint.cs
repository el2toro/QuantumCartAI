namespace Catalog.API.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            return Results.Ok(result.Products);
        })
       .WithDisplayName("GetCatalog")
       .WithDescription("GetCatalog")
       .Produces(StatusCodes.Status200OK)
       .WithSummary("Get Catalog");
    }
}
