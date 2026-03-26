using BuildingBlocks.CQRS;
using MassTransit;

namespace Ordering.Application.Handlers.Commands;

public record CancelOrderCommand() : ICommand<CancelOrderResult>;
public record CancelOrderResult();

public class CancelOrderHandler(IPublishEndpoint publishEndpoin) : ICommandHandler<CancelOrderCommand, CancelOrderResult>
{
    public async Task<CancelOrderResult> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        // await publishEndpoin.Publish<OrderCanceledEvent>("result", cancellationToken);
        return new CancelOrderResult();
    }
}
