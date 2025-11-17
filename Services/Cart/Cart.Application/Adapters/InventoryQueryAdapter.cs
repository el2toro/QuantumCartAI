using Cart.Domain.Ports;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Text.Json;
//using QuantumCartAI.Service.Cart.proto;

namespace Cart.Application.Adapters;

/// <summary>
/// Adapter: Calls Inventory Service via gRPC (or HTTP fallback).
/// </summary>
public class InventoryQueryAdapter : IInventoryQuery
{
    //private readonly InventoryQueryService.InventoryQueryClient _grpcClient;  // Generated from .proto
    private readonly IHttpClientFactory _httpFactory;
    private readonly ILogger<InventoryQueryAdapter> _logger;

    //public InventoryQueryAdapter(InventoryQueryService.InventoryQueryClient grpcClient, IHttpClientFactory httpFactory, ILogger<InventoryQueryAdapter> logger)
    //{
    //    _grpcClient = grpcClient;
    //    _httpFactory = httpFactory;
    //    _logger = logger;
    //}

    public async Task<Quantity> GetAtpAsync(SkuId skuId)
    {
        try
        {
            //  var request = new GetAtpRequest { SkuId = skuId.Value };
            //   var response = await _grpcClient.GetAtpAsync(new());
            return Quantity.From(new());
        }
        catch (RpcException ex) when (true)
        {
            _logger.LogWarning(ex, "Inventory gRPC down, falling back to HTTP");
            return await GetAtpHttpFallback(skuId);
        }
    }

    private async Task<Quantity> GetAtpHttpFallback(SkuId skuId)
    {
        var client = _httpFactory.CreateClient("Inventory");
        var response = await client.GetAsync($"/api/inventory/atp/{skuId.Value}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var atp = JsonSerializer.Deserialize<int>(json);
        return Quantity.From(atp);
    }
}
