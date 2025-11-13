namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(IEnumerable<Guid>? CategoryIds);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog", async (GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
       .WithDisplayName("GetCatalog")
       .WithDescription("GetCatalog")
       .Produces<GetProductsResponse>(StatusCodes.Status200OK)
       .WithSummary("Get Catalog");
    }
}
