using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices", SchemaNames.Ordering);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(
                invoiceId => invoiceId.Value,
                value => InvoiceId.Of(value.ToString()))
            .ValueGeneratedNever();

        // Foreign Key to Order
        builder.Property(i => i.OrderId)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Of(value.ToString()))
            .IsRequired();

        // Navigation property
        builder.HasOne(i => i.Order)
            .WithMany(o => o.Invoices)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Invoice number (unique)
        builder.Property(i => i.InvoiceNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(i => i.InvoiceNumber).IsUnique();

        // Amounts
        builder.Property(i => i.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        //builder.Property(i => i.TaxAmount)
        //    .HasPrecision(18, 2);

        // Currency
        builder.Property(i => i.Currency)
            .HasConversion(
                currency => currency.Code,
                code => Currency.FromCode(code))
            .HasMaxLength(3)
            .IsRequired();

        // Status
        builder.Property(i => i.Status)
            .HasConversion(
                status => status.ToString(),
                v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v))
            .HasMaxLength(50);

        // Dates
        builder.Property(i => i.IssueDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(i => i.PaidDate)
            .HasColumnType("timestamp with time zone");

        // Indexes
        builder.HasIndex(i => i.OrderId);
        builder.HasIndex(i => i.InvoiceNumber);
        builder.HasIndex(i => i.Status);
        builder.HasIndex(i => i.IssueDate);
    }
}
