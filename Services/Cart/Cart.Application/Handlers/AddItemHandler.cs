using Cart.Application.Dtos;
using Cart.Application.Queries;
using Cart.Domain.Interfaces;
using Cart.Domain.Ports;
using Cart.Domain.ValueObjects;
using MediatR;

namespace Cart.Application.Handlers;

public record AddItemCommand(
    Guid CartId,
    string SkuId,
    int Quantity
) : IRequest<CartDto>;

public record AddItemResult();

public class AddItemHandler : IRequestHandler<AddItemCommand, CartDto>
{
    private readonly ICartRepository _repository;
    private readonly IInventoryQuery _inventory;
    private readonly ICartQueryService _query;

    public AddItemHandler(
        ICartRepository repository,
        IInventoryQuery inventory,
        ICartQueryService query)
    {
        _repository = repository;
        _inventory = inventory;
        _query = query;
    }

    public async Task<CartDto> Handle(AddItemCommand request, CancellationToken ct)
    {
        var cart = await _repository.GetByIdAsync(request.CartId)
                   ?? new Domain.Entities.Cart(new CartId(request.CartId));

        cart.AddItem(
            skuId: new SkuId(request.SkuId),
            requestedQty: Quantity.From(request.Quantity),
            inventory: _inventory,
            unitPrice: new Money(99.99m) // ← will come from Pricing service later
        );

        await _repository.SaveAsync(cart);

        // Return fresh read model
        return await _query.GetCartAsync(request.CartId);
    }
}