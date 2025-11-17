using Cart.Domain.ValueObjects;

namespace Cart.Domain.Ports;
/// <summary>
/// Outbound port: Query ATP from Inventory Service.
/// Async to support gRPC/HTTP call in application layer.
/// </summary>
public interface IInventoryQuery
{
    /// <summary>
    /// Get Available-to-Promise for SKU.
    /// Returns Quantity.Zero if OOS.
    /// </summary>
    Task<Quantity> GetAtpAsync(SkuId skuId);
}
