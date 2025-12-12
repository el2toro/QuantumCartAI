namespace Cart.Application.Dtos;

public record AppliedDiscountDto(
    string DiscountId,
    double Amount,
    bool IsPercentage,
    string CouponCode
);
