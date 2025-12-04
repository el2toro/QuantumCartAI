using Ordering.Domain.Entities;


namespace Ordering.Application.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<Order> Orders { get; }
    //DbSet<OrderItem> OrderItems { get; }
    //DbSet<Invoice> Invoices { get; }
    //DbSet<Payment> Payments { get; }
    //DbSet<Shipment> Shipments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
