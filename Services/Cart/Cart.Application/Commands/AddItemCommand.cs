using BuildingBlocks.CQRS;
using Cart.Application.Dtos;

namespace Cart.Application.Commands;

public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity) : ICommand<AddItemResult>;
public record AddItemResult(CartDto Cart);

