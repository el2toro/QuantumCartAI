namespace Discount.gRPC.Models;

public class Discount
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Amount { get; set; }

    public bool IsPercentage { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? CouponCode { get; set; }

    public int? MaxUsage { get; set; }

    public int CurrentUsage { get; set; }

    public decimal? MinPurchaseAmount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    public bool IsValid()
    {
        var now = DateTime.UtcNow;
        return IsActive &&
               now >= StartDate &&
               (EndDate == null || now <= EndDate) &&
               (MaxUsage == null || CurrentUsage < MaxUsage);
    }

    public decimal ApplyDiscount(decimal originalPrice)
    {
        if (!IsValid() || (MinPurchaseAmount.HasValue && originalPrice < MinPurchaseAmount.Value))
            return originalPrice;

        if (IsPercentage)
            return originalPrice * (1 - (decimal)Amount / 100);
        else
            return Math.Max(originalPrice - (decimal)Amount, 0);
    }
}

