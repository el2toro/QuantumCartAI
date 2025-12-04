using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Services;

public interface IOrderPricingService
{
    //Task<OrderPrice> CalculateOrderPriceAsync(
    //    IEnumerable<OrderItem> items,
    //    Address shippingAddress,
    //    CustomerId customerId,
    //    string discountCode = null,
    //    CancellationToken cancellationToken = default);

    //Task<OrderPrice> CalculateOrderPriceAsync(
    //    Order order,
    //    string discountCode = null,
    //    CancellationToken cancellationToken = default);
}

//public class OrderPricingService : IOrderPricingService
//{
//    private readonly IProductPriceService _productPriceService;
//    private readonly IDiscountService _discountService;
//    private readonly ITaxService _taxService;

//    public OrderPricingService(
//        IProductPriceService productPriceService,
//        IDiscountService discountService,
//        ITaxService taxService)
//    {
//        _productPriceService = productPriceService;
//        _discountService = discountService;
//        _taxService = taxService;
//    }

//    public async Task<OrderPrice> CalculateOrderPriceAsync(
//        IEnumerable<OrderItem> items,
//        Address shippingAddress,
//        CustomerId customerId,
//        string discountCode,
//        CancellationToken cancellationToken = default)
//    {
//        var subtotal = 0m;
//        var itemPrices = new List<OrderItemPrice>();

//        // Calculate product prices
//        foreach (var item in items)
//        {
//            var currentPrice = await _productPriceService.GetCurrentPriceAsync(
//                item.ProductId,
//                cancellationToken);

//            if (currentPrice != item.UnitPrice)
//            {
//                // Price has changed since order creation
//                // Log this for audit purposes
//            }

//            var itemTotal = currentPrice * item.Units;
//            subtotal += itemTotal;

//            itemPrices.Add(new OrderItemPrice(
//                item.ProductId,
//                currentPrice,
//                item.Units,
//                itemTotal));
//        }

//        // Apply discounts
//        var discountAmount = 0m;
//        if (!string.IsNullOrEmpty(discountCode))
//        {
//            discountAmount = await _discountService.CalculateDiscountAsync(
//                subtotal,
//                discountCode,
//                customerId,
//                cancellationToken);
//        }

//        var totalAfterDiscount = subtotal - discountAmount;

//        // Calculate tax
//        var taxAmount = await _taxService.CalculateTaxAsync(
//            totalAfterDiscount,
//            shippingAddress,
//            cancellationToken);

//        var shippingCost = await CalculateShippingCostAsync(
//            items,
//            shippingAddress,
//            cancellationToken);

//        var grandTotal = totalAfterDiscount + taxAmount + shippingCost;

//        return new OrderPrice(
//            subtotal,
//            discountAmount,
//            taxAmount,
//            shippingCost,
//            grandTotal);
//    }

//    private async Task<decimal> CalculateShippingCostAsync(
//        IEnumerable<OrderItem> items,
//        Address address,
//        CancellationToken cancellationToken)
//    {
//        // Complex shipping calculation logic
//        // Could integrate with shipping carrier APIs
//        return 9.99m; // Simplified for example
//    }
//}

// Ordering.Domain/Services/OrderValidationService.cs
//public class OrderValidationService : IOrderValidationService
//{
//    private readonly IStockService _stockService;
//    private readonly IFraudDetectionService _fraudDetectionService;

//    public OrderValidationService(
//        IStockService stockService,
//        IFraudDetectionService fraudDetectionService)
//    {
//        _stockService = stockService;
//        _fraudDetectionService = fraudDetectionService;
//    }

//    public async Task<OrderValidationResult> ValidateOrderAsync(
//        Order order,
//        CancellationToken cancellationToken = default)
//    {
//        var validationErrors = new List<string>();

//        // Check stock availability
//        foreach (var item in order.OrderItems)
//        {
//            var stockAvailable = await _stockService.CheckStockAsync(
//                item.ProductId,
//                item.Units,
//                cancellationToken);

//            if (!stockAvailable)
//            {
//                validationErrors.Add(
//                    $"Product {item.ProductName} has insufficient stock. Requested: {item.Units}");
//            }
//        }

//        // Fraud detection
//        var fraudCheck = await _fraudDetectionService.CheckForFraudAsync(
//            order.CustomerId,
//            order.TotalAmount,
//            order.BillingAddress,
//            order.ShippingAddress,
//            cancellationToken);

//        if (fraudCheck.IsSuspicious)
//        {
//            validationErrors.Add($"Order flagged for review: {fraudCheck.Reason}");
//        }

//        // Business rule validation
//        if (order.TotalAmount <= 0)
//        {
//            validationErrors.Add("Order total must be greater than zero");
//        }

//        if (order.OrderItems.Count == 0)
//        {
//            validationErrors.Add("Order must contain at least one item");
//        }

//        return new OrderValidationResult(
//            validationErrors.Count == 0,
//            validationErrors);
//    }
//}
