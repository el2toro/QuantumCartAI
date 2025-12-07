using Microsoft.EntityFrameworkCore;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;
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

    public async Task<Order> GetOrderByIdAsync(OrderId orderId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(CustomerId customerId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }
}
