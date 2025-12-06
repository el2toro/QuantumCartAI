using BuildingBlocks.CQRS;
using Mapster;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;

namespace Ordering.Application.Handlers.Queries;

public record GetOrdersQuery(Guid CustomerId) : IQuery<GetOrdersResult>;
public record GetOrdersResult(IEnumerable<OrderDetailsDto> Orders);

public class GetOrdersHandler(IOrderingRepository orderingRepository)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderingRepository.GetOrdersAsync(query.CustomerId, cancellationToken);
        var result = orders.Adapt<IEnumerable<OrderDetailsDto>>();
        return new GetOrdersResult(result);
    }
}
