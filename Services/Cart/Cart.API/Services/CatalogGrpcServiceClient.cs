using Cart.Application.Dtos;
using Cart.Application.Interfaces;
using Catalog.gRPC;

namespace Cart.API.Services;

public class CatalogGrpcServiceClient(CatalogQueryService.CatalogQueryServiceClient catalogQueryServiceClient) : ICatalogGrpcService
{
    public async Task<ProductQueryDto> GetProduct(Guid productId)
    {
        var request = new CatalogQueryRequest { Id = productId.ToString() };
        var response = await catalogQueryServiceClient.GetProductAsync(request);

        return new ProductQueryDto(response.ProductExists);
    }

    public async Task<bool> UpdateStock(Guid productId, int quantity)
    {
        var request = new CatalogStockUpdateRequest { Id = productId.ToString(), Quantity = quantity };
        var response = await catalogQueryServiceClient.UpdateStockAsync(request);

        return response.StockUpdated;
    }
}
