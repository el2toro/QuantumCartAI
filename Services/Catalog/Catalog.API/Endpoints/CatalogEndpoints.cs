using Carter;
using MediatR;

namespace Catalog.API.Endpoints;

public class CatalogEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("tenants/{tenantId}/catalog", (ISender sender) =>
        {

        })
        .WithDisplayName("GetCatalog")
        .WithDescription("GetCatalog")
        .Produces(StatusCodes.Status200OK)
        .WithSummary("Get Catalog");
    }
}
