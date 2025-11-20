using Cart.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Application.Handlers.Commands;

public record CreateCartCommand(Guid CartId, Currency Currency) : IRequest<CreateCartResult>;
public record CreateCartResult();

public class CreateCartHandler(IDistributedCache distributedCache)
    : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Domain.Entities.Cart(
            new Domain.ValueObjects.CartId(request.CartId), request.Currency);


        var existingCart = await distributedCache.GetStringAsync(request.CartId.ToString());
        if (existingCart is not null)
        {
            return JsonSerializer.Deserialize<CreateCartResult>(existingCart)!;
        }

        return new CreateCartResult();
    }
}
