using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Interfaces;

public interface IOrderingRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync(CustomerId customerId, CancellationToken cancellationToken);
    Task<Order> GetOrderByIdAsync(OrderId orderId, CancellationToken cancellationToken);
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
}
