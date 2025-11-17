using Cart.Application.Dtos;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Application.Queries;

public class CartQueryService : ICartQueryService
{
    private readonly IDistributedCache _redis;

    public CartQueryService(IDistributedCache redis)
    {
        _redis = redis;
    }

    public async Task<CartDto> GetCartAsync(Guid cartId)
    {
        var json = await _redis.GetStringAsync($"cart:{cartId}");
        if (string.IsNullOrEmpty(json))
            return new CartDto(cartId, new(), Money.Zero, null, Money.Zero, Money.Zero, "no promotions", 0);

        return JsonSerializer.Deserialize<CartDto>(json)!;
    }
}
