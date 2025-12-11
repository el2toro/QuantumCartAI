using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Discount> Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Discount>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.HasIndex(d => d.ProductId);
            entity.HasIndex(d => d.CouponCode);
            entity.HasIndex(d => new { d.IsActive, d.StartDate, d.EndDate });

            entity.Property(d => d.Amount)
                .HasPrecision(18, 2);

            entity.Property(d => d.MinPurchaseAmount)
                .HasPrecision(18, 2);
        });
    }
}
