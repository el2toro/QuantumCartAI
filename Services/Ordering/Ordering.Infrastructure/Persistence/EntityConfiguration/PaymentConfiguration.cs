using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments", SchemaNames.Ordering);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                paymentId => paymentId.Value,
                value => PaymentId.Of(value.ToString()))
            .ValueGeneratedNever();

        // Foreign Key to Order
        builder.Property(p => p.OrderId)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Of(value.ToString()))
            .IsRequired();

        // Navigation property
        builder.HasOne(p => p.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Other properties...
        builder.Property(p => p.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Currency)
            .HasConversion(
                currency => currency.Code,
                code => Currency.FromCode(code))
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion(
                status => status.ToString(),
                v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v))
            .HasMaxLength(20);

        // Indexes
        builder.HasIndex(p => p.OrderId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.PaymentDate);
        builder.HasIndex(p => p.GatewayTransactionId).IsUnique();
    }
}
