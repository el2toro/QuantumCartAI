using Discount.gRPC.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Repositories;

public interface IDiscountRepository
{
    Task<Models.Discount?> GetByIdAsync(Guid discountId);
    Task<IEnumerable<Models.Discount>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<Models.Discount>> GetActiveDiscountsAsync(int pageNumber, int pageSize);
    Task<Models.Discount> CreateAsync(Models.Discount discount);
    Task<Models.Discount?> UpdateAsync(Guid discountId, Models.Discount discount);
    Task<bool> DeleteAsync(Guid discountId);
    Task<bool> ExistsAsync(Guid discountId);
    Task<Models.Discount?> GetByCouponCodeAsync(string couponCode);
    Task<int> GetTotalActiveCountAsync();
}

public class DiscountRepository(DiscountDbContext dbContext) : IDiscountRepository
{
    public async Task<Models.Discount> CreateAsync(Models.Discount discount)
    {
        dbContext.Discounts.Add(discount);
        await dbContext.SaveChangesAsync();
        return discount;
    }

    public async Task<bool> DeleteAsync(Guid discountId)
    {
        var discount = await GetByIdAsync(discountId);
        if (discount == null)
            return false;

        dbContext.Discounts.Remove(discount);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid discountId)
    {
        return await dbContext.Discounts
                .AnyAsync(d => d.Id == discountId);
    }

    public async Task<IEnumerable<Models.Discount>> GetActiveDiscountsAsync(int pageNumber, int pageSize)
    {
        var now = DateTime.UtcNow;

        return await dbContext.Discounts
            .Where(d => d.IsActive &&
                       d.StartDate <= now &&
                       (d.EndDate == null || d.EndDate >= now) &&
                       (d.MaxUsage == null || d.CurrentUsage < d.MaxUsage))
            .OrderByDescending(d => d.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Models.Discount?> GetByCouponCodeAsync(string couponCode)
    {
        return await dbContext.Discounts
                 .FirstOrDefaultAsync(d => d.CouponCode == couponCode && d.IsValid());
    }

    public async Task<Models.Discount?> GetByIdAsync(Guid discountId)
    {
        return await dbContext.Discounts
                .FirstOrDefaultAsync(d => d.Id == discountId);
    }

    public async Task<IEnumerable<Models.Discount>> GetByProductIdAsync(Guid productId)
    {
        return await dbContext.Discounts
               .Where(d => d.ProductId == productId)
               .OrderByDescending(d => d.CreatedDate)
               .ToListAsync();
    }

    public async Task<int> GetTotalActiveCountAsync()
    {
        var now = DateTime.UtcNow;

        return await dbContext.Discounts
            .CountAsync(d => d.IsActive &&
                           d.StartDate <= now &&
                           (d.EndDate == null || d.EndDate >= now) &&
                           (d.MaxUsage == null || d.CurrentUsage < d.MaxUsage));
    }

    public async Task<Models.Discount?> UpdateAsync(Guid discountId, Models.Discount discount)
    {
        var existingDiscount = await GetByIdAsync(discountId);
        if (existingDiscount == null)
            return null;

        existingDiscount.ProductId = discount.ProductId;
        existingDiscount.ProductName = discount.ProductName;
        existingDiscount.Description = discount.Description;
        existingDiscount.Amount = discount.Amount;
        existingDiscount.IsPercentage = discount.IsPercentage;
        existingDiscount.StartDate = discount.StartDate;
        existingDiscount.EndDate = discount.EndDate;
        existingDiscount.CouponCode = discount.CouponCode;
        existingDiscount.MaxUsage = discount.MaxUsage;
        existingDiscount.CurrentUsage = discount.CurrentUsage;
        existingDiscount.MinPurchaseAmount = discount.MinPurchaseAmount;
        existingDiscount.IsActive = discount.IsActive;
        existingDiscount.ModifiedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return existingDiscount;
    }
}
