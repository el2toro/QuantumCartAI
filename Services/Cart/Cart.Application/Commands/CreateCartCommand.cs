using Cart.Domain.ValueObjects;
using MediatR;

namespace Cart.Application.Commands;

public record CreateCartCommand(Guid CartId, Currency Currency) : IRequest<CreateCartResult>;
public record CreateCartResult();

