namespace Cart.Application.Dtos;

public record AppliedDiscountDto(
    string DiscountId,
    int Amount,
    bool IsPercentage,
    string CouponCode
);
