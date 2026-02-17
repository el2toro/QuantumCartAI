using Catalog.gRPC;
using Grpc.Core;

namespace Catalog.API.Services;

public class CatalogGrpcService(IProductRepository productRepository)
    : CatalogQueryService.CatalogQueryServiceBase
{
    public override async Task<CatalogQueryResponse> GetProduct(CatalogQueryRequest request, ServerCallContext context)
    {
        var product = await productRepository.GetProductById(Guid.Parse(request.Id), CancellationToken.None);

        bool productInStock = product is not null && product.Quantity > 0;

        return new CatalogQueryResponse() { ProductExists = productInStock };
    }

    public override async Task<CatalogStockUpdateResponse> UpdateStock(CatalogStockUpdateRequest request, ServerCallContext context)
    {
        bool stockUpdated = await productRepository.UpdateStock(Guid.Parse(request.Id), request.Quantity, CancellationToken.None);
        return new CatalogStockUpdateResponse() { StockUpdated = stockUpdated };
    }
}
