using Cart.Application.Dtos;

namespace Cart.Application.Queries;

public interface ICartQueryService
{
    Task<CartDto> GetCartAsync(Guid cartId);
}
