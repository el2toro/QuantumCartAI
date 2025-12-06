using Ordering.Domain.Entities;

namespace Ordering.Application.Interfaces;

public interface IOrderingRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync(Guid customerId, CancellationToken cancellationToken);
    Task<Order> GetOrderByIdAsync(Guid customerId, Guid orderId, CancellationToken cancellationToken);
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
}
