using BuildingBlocks.CQRS;
using Mapster;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Handlers.Queries;

public record GetOrdersQuery(Guid CustomerId) : IQuery<GetOrdersResult>;
public record GetOrdersResult(IEnumerable<OrderDetailsDto> Orders);

public class GetOrdersHandler(IOrderingRepository orderingRepository)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderingRepository.GetOrdersAsync(CustomerId.Of(query.CustomerId.ToString()), cancellationToken);

        var result = orders.Adapt<IEnumerable<OrderDetailsDto>>();

        return new GetOrdersResult(result);
    }
}
