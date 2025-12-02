using BuildingBlocks.CQRS;
using MassTransit;
using Ordering.Domain.Events;

namespace Ordering.Application.Handlers.Commands;

public record CreateOrderCommand() : ICommand<CreateOrderResult>;
public record CreateOrderResult();
public class CreateOrderHandler(IPublishEndpoint publishedEndpoint) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await publishedEndpoint.Publish<OrderCreatedEvent>("result", cancellationToken);
        return new CreateOrderResult();
    }
}
