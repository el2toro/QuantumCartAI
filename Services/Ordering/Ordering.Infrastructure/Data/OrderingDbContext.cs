using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence.EntityConfiguration;
using System.Reflection;

namespace Ordering.Infrastructure.Data;

public class OrderingDbContext : DbContext
{
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<OrderStatusHistory> OrderStatusHistories => Set<OrderStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Optional: Set default schema
        modelBuilder.HasDefaultSchema(SchemaNames.Ordering);

        // Optional: Configure decimal precision globally
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }

        // Optional: Configure string properties max length globally
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string)))
        {
            if (property.GetMaxLength() == null)
            {
                property.SetMaxLength(200); // Default max length for strings
            }
        }

        // Optional: Configure DateTime properties
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        {
            property.SetColumnType("timestamp without time zone");
        }

        // Configure delete behavior for all relationships
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // Enable sensitive data logging in development
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        // Enable detailed errors
        optionsBuilder.EnableDetailedErrors();
    }

    // Override SaveChanges to update audit fields
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    private void UpdateAuditFields()
    {
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.Entity is Entity &&
        //            (e.State == EntityState.Added || e.State == EntityState.Modified));

        //    var now = DateTime.UtcNow;

        //    foreach (var entry in entries)
        //    {
        //        // Set CreatedAt for new entities
        //        if (entry.State == EntityState.Added)
        //        {
        //            if (entry.Entity is Order order)
        //            {
        //                order.CreatedAt = now;
        //                order.UpdatedAt = now;
        //            }
        //            else if (entry.Entity is Entity entityWithAudit)
        //            {
        //                // Use reflection for other entities with audit fields
        //                var createdAtProp = entry.Entity.GetType().GetProperty("CreatedAt");
        //                var updatedAtProp = entry.Entity.GetType().GetProperty("UpdatedAt");

        //                createdAtProp?.SetValue(entry.Entity, now);
        //                updatedAtProp?.SetValue(entry.Entity, now);
        //            }
        //        }

        //        // Set UpdatedAt for modified entities
        //        if (entry.State == EntityState.Modified)
        //        {
        //            if (entry.Entity is Order order)
        //            {
        //                order.UpdatedAt = now;
        //            }
        //            else
        //            {
        //                var updatedAtProp = entry.Entity.GetType().GetProperty("UpdatedAt");
        //                updatedAtProp?.SetValue(entry.Entity, now);
        //            }
        //        }
        //    }
    }
}
