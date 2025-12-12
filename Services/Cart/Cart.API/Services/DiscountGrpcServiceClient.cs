using Cart.Application.Dtos;
using Cart.Application.Interfaces;
using DiscountService.gRPC;
using static DiscountService.gRPC.DiscountService;

namespace Cart.API.Services;

public class DiscountGrpcServiceClient(DiscountServiceClient discountServiceClient) : IDiscountGrpcService
{
    public async Task<AppliedDiscountDto> GetDiscountByCouponCode(string couponCode)
    {
        var request = new CouponRequest() { CouponCode = couponCode };
        var result = await discountServiceClient.GetDiscountByCouponAsync(request);

        return new AppliedDiscountDto(result.DiscountId, result.Amount, result.IsPercentage, result.CouponCode);
    }

    public async Task GetDiscountById(Guid discountId)
    {
        var request = new GetDiscountRequest() { DiscountId = discountId.ToString() };

        var result = await discountServiceClient.GetDiscountAsync(request);

        //TODO: return discount
    }

    public async Task<IEnumerable<AppliedDiscountDto>> GetProductDiscounts(Guid productId)
    {
        var request = new GetProductDiscountsRequest() { ProductId = productId.ToString() };
        var result = await discountServiceClient.GetProductDiscountsAsync(request);

        return result.Discounts.Select(grpc =>
             new AppliedDiscountDto(
                 grpc.DiscountId,
                 grpc.Amount,
                 grpc.IsPercentage,
                 grpc.CouponCode));
    }
}
