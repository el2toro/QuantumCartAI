using Cart.Domain.Interfaces;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Infrastructure.Repositories;

public class CartRepository(IDistributedCache distributedCache) : ICartRepository
{
    public async Task<Domain.Entities.Cart?> GetByIdAsync(Guid cartId)
    {
        var cartString = await distributedCache.GetStringAsync(cartId.ToString());

        return string.IsNullOrWhiteSpace(cartString)
             ? JsonSerializer.Deserialize<Domain.Entities.Cart>(cartString!)
             : new Domain.Entities.Cart(CartId.From(Guid.Empty.ToString()), Currency.None);
    }

    public async Task SaveAsync(Domain.Entities.Cart cart)
    {
        if (cart is not null)
        {
            await distributedCache.SetStringAsync(cart.CustomerId.ToString(), JsonSerializer.Serialize(cart));
        }
    }
}
