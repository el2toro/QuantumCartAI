using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Domain.ValueObjects;

namespace Cart.Application.Commands;

public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity, Currency Currency) : ICommand<AddItemResult>;
public record AddItemResult(CartDto Cart);

