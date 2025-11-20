using Cart.Application.Dtos;

namespace Cart.Application.Interfaces;

public interface ICatalogGrpcService
{
    Task<ProductQueryDto> GetProduct(Guid productId);
}
