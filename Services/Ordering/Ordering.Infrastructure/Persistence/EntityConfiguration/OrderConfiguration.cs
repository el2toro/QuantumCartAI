using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Table configuration
        builder.ToTable("Orders", SchemaNames.Ordering);

        // Primary Key
        builder.HasKey(o => o.Id);

        // Property configurations
        builder.Property(o => o.Id)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Of(value.ToString()))
            .ValueGeneratedNever()
            .HasColumnName("Id")
            .IsRequired();

        // CustomerId
        builder.Property(o => o.CustomerId)
            .HasConversion(
                customerId => customerId.Value,
                value => CustomerId.Of(value.ToString()))
            .HasColumnName("CustomerId")
            .IsRequired();

        // OrderNumber (Value Object)
        builder.OwnsOne(o => o.OrderNumber, on =>
        {
            on.Property(n => n.Value)
                .HasColumnName("OrderNumber")
                .HasMaxLength(50)
                .IsRequired();

            on.HasIndex(n => n.Value)
                .IsUnique()
                .HasDatabaseName("IX_Orders_OrderNumber");
        });

        // ShippingAddress (Value Object)
        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(p => p.Street)
                .HasColumnName("ShippingStreet")
                .HasMaxLength(200)
                .IsRequired();

            sa.Property(p => p.City)
                .HasColumnName("ShippingCity")
                .HasMaxLength(100)
                .IsRequired();

            sa.Property(p => p.State)
                .HasColumnName("ShippingState")
                .HasMaxLength(50)
                .IsRequired();

            sa.Property(p => p.Country)
                .HasColumnName("ShippingCountry")
                .HasMaxLength(50)
                .IsRequired();

            sa.Property(p => p.ZipCode)
                .HasColumnName("ShippingZipCode")
                .HasMaxLength(20)
                .IsRequired();
        });

        // BillingAddress (Value Object) - separate from shipping address
        builder.OwnsOne(o => o.BillingAddress, ba =>
        {
            ba.Property(p => p.Street)
                .HasColumnName("BillingStreet")
                .HasMaxLength(200);

            ba.Property(p => p.City)
                .HasColumnName("BillingCity")
                .HasMaxLength(100);

            ba.Property(p => p.State)
                .HasColumnName("BillingState")
                .HasMaxLength(50);

            ba.Property(p => p.Country)
                .HasColumnName("BillingCountry")
                .HasMaxLength(50);

            ba.Property(p => p.ZipCode)
                .HasColumnName("BillingZipCode")
                .HasMaxLength(20);
        });

        // Status
        builder.Property(o => o.Status)
            .HasConversion(
                status => status.ToString(),
                value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value))
            .HasColumnName("Status")
            .HasMaxLength(50)
            .IsRequired();

        // PaymentStatus
        builder.Property(o => o.PaymentStatus)
            .HasConversion(
                status => status.ToString(),
                value => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), value))
            .HasColumnName("PaymentStatus")
            .HasMaxLength(50)
            .IsRequired();

        // Monetary properties
        builder.Property(o => o.TotalAmount)
            .HasColumnName("TotalAmount")
            .HasPrecision(18, 2)
            .IsRequired();

        // Currency
        builder.Property(o => o.Currency)
            .HasConversion(
                currency => currency.Code,
                code => Currency.FromCode(code))
            .HasColumnName("Currency")
            .HasMaxLength(3)
            .IsRequired();

        // Timestamps
        builder.Property(o => o.OrderDate)
            .HasColumnName("OrderDate")
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(o => o.PaidDate)
            .HasColumnName("PaidDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(o => o.ShippedDate)
            .HasColumnName("ShippedDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(o => o.DeliveredDate)
            .HasColumnName("DeliveredDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(o => o.CancelledDate)
            .HasColumnName("CancelledDate")
            .HasColumnType("timestamp without time zone");

        // Other properties
        builder.Property(o => o.CancellationReason)
            .HasColumnName("CancellationReason")
            .HasMaxLength(500);

        builder.Property(o => o.CustomerNotes)
            .HasColumnName("CustomerNotes")
            .HasMaxLength(1000);

        // Audit properties
        builder.Property(o => o.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp without time zone")
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(o => o.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("timestamp without time zone")
            .IsRequired()
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAddOrUpdate();


        // Configure relationships

        // OrderItems (one-to-many)
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Payments (one-to-many)
        builder.HasMany(o => o.Payments)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        // Shipments (one-to-many)
        builder.HasMany(o => o.Shipments)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        // Invoices (one-to-many)
        builder.HasMany(o => o.Invoices)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        // StatusHistory (one-to-many)
        builder.HasMany(o => o.StatusHistory)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for performance
        builder.HasIndex(o => o.CustomerId)
            .HasDatabaseName("IX_Orders_CustomerId");

        builder.HasIndex(o => o.Status)
            .HasDatabaseName("IX_Orders_Status");

        builder.HasIndex(o => o.PaymentStatus)
            .HasDatabaseName("IX_Orders_PaymentStatus");

        builder.HasIndex(o => o.OrderDate)
            .HasDatabaseName("IX_Orders_OrderDate");

        builder.HasIndex(o => o.PaidDate)
            .HasDatabaseName("IX_Orders_PaidDate");

        builder.HasIndex(o => new { o.CustomerId, o.OrderDate })
            .HasDatabaseName("IX_Orders_CustomerId_OrderDate");

        builder.HasIndex(o => new { o.Status, o.OrderDate })
            .HasDatabaseName("IX_Orders_Status_OrderDate");

        // Partial indexes for common queries
        //builder.HasIndex(o => o.OrderDate)
        //    .HasFilter("[Status] NOT IN ('Cancelled', 'Refunded')")
        //    .HasDatabaseName("IX_Orders_Active_OrderDate");

        //builder.HasIndex(o => o.CustomerId)
        //    .HasFilter("[Status] IN ('Confirmed', 'Processing', 'Shipped')")
        //    .HasDatabaseName("IX_Orders_CustomerId_Active");

        // Query filter for soft delete (if implemented)
        // builder.HasQueryFilter(o => !o.IsDeleted);

        // Ignore domain events (not stored in database)
        builder.Ignore(o => o.DomainEvents);

        // Configure table partitioning (optional, for large tables)
        // builder.ToTable("Orders")
        //     .HasPartitionKey(o => o.OrderDate.Year);
    }
}

// Schema names constants
public static class SchemaNames
{
    public const string Ordering = "ordering";
    public const string Catalog = "catalog";
    public const string Payment = "payment";
}
