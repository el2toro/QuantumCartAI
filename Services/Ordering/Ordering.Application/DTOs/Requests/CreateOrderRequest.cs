using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Application.DTOs.Requests;

public record CreateOrderRequest
{
    [Required]
    public Guid CustomerId { get; init; }
    public Address ShippingAddress { get; init; }
    public Address BillingAddress { get; init; }
    public string OrderNumber { get; init; }
    public string Currency { get; init; }

    // [Required]
    // [StringLength(20)]
    // public string ShippingMethod { get; init; } = "standard";

    // [Required]
    // [StringLength(20)]
    // public string PaymentMethod { get; init; }

    // public PaymentDetailsRequest PaymentDetails { get; init; }

    // [StringLength(50)]
    //public string DiscountCode { get; init; }

    // [StringLength(500)]
    public string CustomerNotes { get; init; }
    public List<OrderItemDto> OrderItems { get; init; }

    //public MarketingPreferencesRequest MarketingPreferences { get; init; }

    //public Dictionary<string, object> Metadata { get; init; } = new();
}
