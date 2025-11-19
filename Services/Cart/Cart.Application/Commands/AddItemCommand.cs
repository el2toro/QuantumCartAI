using Cart.Application.DTOs;
using MediatR;

namespace Cart.Application.Commands;

public record AddItemCommand(Guid? CustomerId, Guid ProductId, int Quantity) : IRequest<AddItemResult>;
public record AddItemResult(bool IsSuccess);

