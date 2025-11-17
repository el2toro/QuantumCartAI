namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
       .WithDisplayName("GetCatalog")
       .WithDescription("GetCatalog")
       .Produces<GetProductsResponse>(StatusCodes.Status200OK)
       .WithSummary("Get Catalog");
    }
}
