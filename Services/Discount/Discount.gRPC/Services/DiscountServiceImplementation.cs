using Discount.gRPC.Repositories;
using DiscountService.gRPC;
using Grpc.Core;
using static DiscountService.gRPC.DiscountService;

namespace Discount.gRPC.Services;

public class DiscountServiceImplementation : DiscountServiceBase
{
    private readonly ILogger<DiscountServiceImplementation> _logger;
    private readonly IDiscountRepository _repository;
    public DiscountServiceImplementation(ILogger<DiscountServiceImplementation> logger, IDiscountRepository discountRepository)
    {
        _logger = logger;
        _repository = discountRepository;
    }

    public override async Task<DiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting discount with ID: {DiscountId}", request.DiscountId);

        var discount = await _repository.GetByIdAsync(Guid.Parse(request.DiscountId));

        if (discount == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ID {request.DiscountId} not found"));

        return MapToDiscountResponse(discount);
    }

    public override async Task<DiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Creating discount for product: {ProductId}", request.ProductId);

        var discount = new Models.Discount
        {
            ProductId = Guid.Parse(request.ProductId),
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount,
            IsPercentage = request.IsPercentage,
            StartDate = DateTime.Parse(request.StartDate),
            EndDate = string.IsNullOrEmpty(request.EndDate) ? null : DateTime.Parse(request.EndDate),
            CouponCode = string.IsNullOrEmpty(request.CouponCode) ? null : request.CouponCode,
            MaxUsage = request.MaxUsage == 0 ? null : request.MaxUsage,
            CurrentUsage = 0,
            MinPurchaseAmount = request.MinPurchaseAmount == 0 ? null : (decimal?)request.MinPurchaseAmount,
            IsActive = true
        };

        var createdDiscount = await _repository.CreateAsync(discount);

        return MapToDiscountResponse(createdDiscount);
    }

    public override async Task<DiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Updating discount with ID: {DiscountId}", request.DiscountId);

        var existingDiscount = await _repository.GetByIdAsync(Guid.Parse(request.DiscountId));

        if (existingDiscount == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ID {request.DiscountId} not found"));

        var discount = new Models.Discount
        {
            ProductId = Guid.Parse(request.ProductId),
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount,
            IsPercentage = request.IsPercentage,
            StartDate = DateTime.Parse(request.StartDate),
            EndDate = string.IsNullOrEmpty(request.EndDate) ? null : DateTime.Parse(request.EndDate),
            CouponCode = string.IsNullOrEmpty(request.CouponCode) ? null : request.CouponCode,
            MaxUsage = request.MaxUsage == 0 ? null : request.MaxUsage,
            CurrentUsage = request.CurrentUsage,
            MinPurchaseAmount = request.MinPurchaseAmount == 0 ? null : (decimal?)request.MinPurchaseAmount,
            IsActive = request.IsActive
        };

        var updatedDiscount = await _repository.UpdateAsync(Guid.Parse(request.DiscountId), discount);

        if (updatedDiscount == null)
            throw new RpcException(new Status(StatusCode.Internal, "Failed to update discount"));

        return MapToDiscountResponse(updatedDiscount);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Deleting discount with ID: {DiscountId}", request.DiscountId);

        var success = await _repository.DeleteAsync(Guid.Parse(request.DiscountId));

        return new DeleteDiscountResponse
        {
            Success = success,
            Message = success ? "Discount deleted successfully" : "Discount not found"
        };
    }

    public override async Task<DiscountListResponse> GetProductDiscounts(GetProductDiscountsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting discounts for product: {ProductId}", request.ProductId);

        var discounts = await _repository.GetByProductIdAsync(Guid.Parse(request.ProductId));
        var discountReplies = discounts.Select(MapToDiscountResponse);

        var reply = new DiscountListResponse();
        reply.Discounts.AddRange(discountReplies);
        reply.TotalCount = discounts.Count();

        return reply;
    }

    public override async Task<DiscountListResponse> GetActiveDiscounts(GetActiveDiscountsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting active discounts - Page: {Page}, Size: {Size}",
            request.PageNumber, request.PageSize);

        var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
        var pageSize = request.PageSize > 0 ? request.PageSize : 10;

        var discounts = await _repository.GetActiveDiscountsAsync(pageNumber, pageSize);
        var totalCount = await _repository.GetTotalActiveCountAsync();
        var discountReplies = discounts.Select(MapToDiscountResponse);

        var reply = new DiscountListResponse();
        reply.Discounts.AddRange(discountReplies);
        reply.TotalCount = totalCount;
        reply.PageNumber = pageNumber;
        reply.PageSize = pageSize;

        return reply;
    }

    public async override Task<DiscountResponse> GetDiscountByCoupon(CouponRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting discount with ID: {DiscountId}", request.CouponCode);

        var discount = await _repository.GetByCouponCodeAsync(request.CouponCode);

        if (discount is not null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with CouponCode {request.CouponCode} not found"));

        return MapToDiscountResponse(discount);
    }

    private DiscountResponse MapToDiscountResponse(Models.Discount discount)
    {
        return new DiscountResponse
        {
            DiscountId = discount.Id.ToString(),
            ProductId = discount.ProductId.ToString(),
            ProductName = discount.ProductName,
            Description = discount.Description,
            Amount = discount.Amount,
            IsPercentage = discount.IsPercentage,
            StartDate = discount.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            EndDate = discount.EndDate?.ToString("yyyy-MM-ddTHH:mm:ss") ?? string.Empty,
            CouponCode = discount.CouponCode ?? string.Empty,
            MaxUsage = discount.MaxUsage ?? 0,
            CurrentUsage = discount.CurrentUsage,
            MinPurchaseAmount = discount.MinPurchaseAmount.HasValue ? (int)discount.MinPurchaseAmount.Value : 0,
            IsActive = discount.IsActive,
            CreatedDate = discount.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            ModifiedDate = discount.ModifiedDate.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }
}
