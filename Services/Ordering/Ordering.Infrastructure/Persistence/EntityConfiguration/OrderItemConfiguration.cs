using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;


namespace Ordering.Infrastructure.Persistence.EntityConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        // Convert OrderItemId to Guid for DB
        builder.Property(oi => oi.Id)
            .HasConversion(
                id => id.Value,      // to database
                value => new OrderItemId(value)); // from database

        builder.Property(oi => oi.ProductId)
          .HasConversion(
              id => id.Value,      // to database
              value => new ProductId(value)); // from database
    }
}
