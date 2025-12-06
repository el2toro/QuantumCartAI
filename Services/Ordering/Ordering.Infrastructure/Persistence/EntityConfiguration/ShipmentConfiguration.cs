using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments", SchemaNames.Ordering);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                shipmentId => shipmentId.Value,
                value => ShipmentId.Of(value.ToString()))
            .ValueGeneratedNever();

        // Foreign Key to Order
        builder.Property(s => s.OrderId)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Of(value.ToString()))
            .IsRequired();

        // Navigation property
        builder.HasOne(s => s.Order)
            .WithMany(o => o.Shipments)
            .HasForeignKey(s => s.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tracking number (unique)
        builder.Property(s => s.TrackingNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(s => s.TrackingNumber).IsUnique();

        // Carrier
        builder.Property(s => s.Carrier)
            .HasMaxLength(100)
            .IsRequired();

        // Status
        builder.Property(s => s.Status)
            .HasConversion(
                status => status.ToString(),
                v => (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), v))
            .HasMaxLength(50);

        // Shipping cost
        builder.Property(s => s.ShippingCost)
            .HasPrecision(18, 2);

        // Timestamps
        builder.Property(s => s.ShippedDate)
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.EstimatedDeliveryDate)
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DeliveredDate)
            .HasColumnType("timestamp without time zone");

        // Indexes
        builder.HasIndex(s => s.OrderId);
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.ShippedDate);
        builder.HasIndex(s => new { s.OrderId, s.Status });
    }
}
