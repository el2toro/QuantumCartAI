using Catalog.API.Interfaces;
using Catalog.gRPC;
using Grpc.Core;

namespace Catalog.API.Services;

public class CatalogGrpcService(IProductRepository productRepository)
    : CatalogQueryService.CatalogQueryServiceBase
{
    public override async Task<CatalogQueryResponse> GetProduct(CatalogQueryRequest request, ServerCallContext context)
    {
        var product = await productRepository.GetProductById(Guid.Parse(request.Id), CancellationToken.None);

        bool productExists = product is not null;

        return new CatalogQueryResponse() { ProductExists = productExists };
    }
}
