using Cart.Application.Dtos;

namespace Cart.Application.Interfaces;

public interface IDiscountGrpcService
{
    Task GetDiscountById(Guid discountId);
    Task<IEnumerable<AppliedDiscountDto>> GetProductDiscounts(Guid productId);
    Task<AppliedDiscountDto> GetDiscountByCouponCode(string couponCode);
}
