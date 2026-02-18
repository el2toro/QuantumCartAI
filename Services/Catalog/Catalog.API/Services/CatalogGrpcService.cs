using Catalog.gRPC;
using Grpc.Core;

namespace Catalog.API.Services;

public class CatalogGrpcService(IProductRepository productRepository)
    : CatalogQueryService.CatalogQueryServiceBase
{
    public override async Task<CatalogQueryResponse> GetProduct(CatalogQueryRequest request, ServerCallContext context)
    {
        var product = await productRepository.GetProductById(Guid.Parse(request.Id), CancellationToken.None);

        return new CatalogQueryResponse() { ProductId = product!.Id.ToString(), Quantity = product.Quantity, Price = (int)product.Price };
    }

    public override async Task<CatalogStockUpdateResponse> UpdateStock(CatalogStockUpdateRequest request, ServerCallContext context)
    {
        bool stockUpdated = await productRepository.UpdateStock(Guid.Parse(request.Id), request.Quantity, CancellationToken.None);
        return new CatalogStockUpdateResponse() { StockUpdated = stockUpdated };
    }
}
