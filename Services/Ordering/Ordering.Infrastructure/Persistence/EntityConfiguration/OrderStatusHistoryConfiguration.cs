using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;
using System.Text.Json;

namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.ToTable("OrderStatusHistories", SchemaNames.Ordering);

        builder.HasKey(sh => sh.Id);

        builder.Property(sh => sh.Id)
            .HasConversion(
                historyId => historyId.Value,
                value => OrderStatusHistoryId.Of(value))
            .ValueGeneratedNever();

        // Foreign Key to Order
        builder.Property(sh => sh.OrderId)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Of(value.ToString()))
            .IsRequired();

        // Navigation property
        builder.HasOne(sh => sh.Order)
            .WithMany(o => o.StatusHistory)
            .HasForeignKey(sh => sh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Status
        builder.Property(sh => sh.Status)
            .HasConversion(
                status => status.ToString(),
                v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v))
            .HasMaxLength(50)
            .IsRequired();

        // Changed by
        builder.Property(sh => sh.ChangedBy)
            .HasMaxLength(100)
            .IsRequired();

        // Notes
        builder.Property(sh => sh.Notes)
            .HasMaxLength(1000);

        // Changed at (with default value)
        builder.Property(sh => sh.ChangedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        // Metadata (JSON)
        builder.Property(sh => sh.Metadata)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, JsonSerializerOptions.Default) ?? new());

        // Indexes
        builder.HasIndex(sh => sh.OrderId);
        builder.HasIndex(sh => new { sh.OrderId, sh.ChangedAt });
        builder.HasIndex(sh => sh.Status);

        // Composite index for common queries
        builder.HasIndex(sh => new { sh.OrderId, sh.Status, sh.ChangedAt })
            .IsDescending(false, false, true);
    }
}