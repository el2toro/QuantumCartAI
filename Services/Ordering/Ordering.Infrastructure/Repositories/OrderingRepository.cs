using Microsoft.EntityFrameworkCore;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderingRepository(OrderingDbContext dbContext)
    : IOrderingRepository
{
    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        var createdOrder = dbContext.Orders.Add(order).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);

        return createdOrder;
    }

    public async Task<Order> GetOrderByIdAsync(Guid customerId, Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders.FindAsync(orderId, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId.Value == customerId)
            .ToListAsync(cancellationToken);
    }
}
