using Cart.Application.Dtos;
using MediatR;

namespace Cart.Application.Commands;

public record AddItemCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity) : IRequest<AddItemResult>;
public record AddItemResult(CartDto Cart);

