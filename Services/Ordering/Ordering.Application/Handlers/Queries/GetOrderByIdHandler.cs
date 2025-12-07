using BuildingBlocks.CQRS;
using Mapster;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Handlers.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<GetOrderByIdResult>;
public record GetOrderByIdResult(OrderDetailsDto Order);

public class GetOrderByIdHandler(IOrderingRepository orderingRepository)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await orderingRepository.GetOrderByIdAsync(OrderId.Of(query.OrderId.ToString()), cancellationToken);
        var result = order.Adapt<OrderDetailsDto>();

        return new GetOrderByIdResult(result);
    }
}
