using Cart.Application.Commands;
using Microsoft.Extensions.Caching.Distributed;
using MediatR;
using System.Text.Json;

namespace Cart.Application.Handlers;

public class CreateCartHandler(IDistributedCache distributedCache)
    : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Domain.Entities.Cart(
            new Domain.ValueObjects.CartId(request.CartId),
             new Domain.ValueObjects.CustomerId(request.CartId));


        var existingCart = await distributedCache.GetStringAsync(request.CartId.ToString());
        if (existingCart is not null)
        {
            return JsonSerializer.Deserialize<CreateCartResult>(existingCart)!;
        }

        return new CreateCartResult();
    }
}
