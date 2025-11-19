using MediatR;

namespace Cart.Application.Commands;

public record CreateCartCommand(Guid CartId) : IRequest<CreateCartResult>;
public record CreateCartResult();

