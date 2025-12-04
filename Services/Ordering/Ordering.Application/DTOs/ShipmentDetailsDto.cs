using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs;

public record ShipmentDetailsDto
{
    public string TrackingNumber { get; init; }
    public string Carrier { get; init; }
    public string ServiceType { get; init; }
    public decimal ShippingCost { get; init; }
    public DateTime ShippedDate { get; init; }
    public DateTime? EstimatedDelivery { get; init; }
    public DateTime? DeliveredDate { get; init; }
    public string DeliveryNotes { get; init; }
    public AddressDto DeliveryAddress { get; init; }
    public List<ShipmentTrackingEventDto> TrackingEvents { get; init; } = new();
}