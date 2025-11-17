using Cart.Application.Dtos;
using Cart.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;

namespace Cart.Application.Queries;

public class CartQueryService : ICartQueryService
{
    private readonly IDatabase _redis;

    //public CartQueryService(IConnectionMultiplexer redis)
    //{
    //    _redis = redis.GetDatabase();
    //}

    public async Task<CartDto> GetCartAsync(Guid cartId)
    {
        // var json = await _redis.StringGetAsync($"cart:{cartId}");
        //  if ("json".IsNullOrEmpty)
        return new CartDto(cartId, new(), Money.Zero, null, Money.Zero, Money.Zero, "no promotions", 0);

        // return JsonSerializer.Deserialize<CartDto>(json)!;
    }
}
