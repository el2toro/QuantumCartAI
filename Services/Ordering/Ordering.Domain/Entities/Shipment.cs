using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Shipment : AggregateRoot<ShipmentId>
{
    public OrderId OrderId { get; private set; }
    public string TrackingNumber { get; private set; }
    public string Carrier { get; private set; }
    public decimal ShippingCost { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime ShippedDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public DateTime? EstimatedDeliveryDate { get; private set; }

    // Navigation property to Order
    public Order Order { get; private set; }

    private Shipment() { }

    public static Shipment Create(
        OrderId orderId,
        string trackingNumber,
        string carrier,
        decimal shippingCost,
        Address deliveryAddress,
        string serviceType)
    {
        return new Shipment
        {
            Id = ShipmentId.Create(),
            OrderId = orderId,
            TrackingNumber = trackingNumber,
            Carrier = carrier,
            ShippingCost = shippingCost,
            Status = ShipmentStatus.InTransit,
            ShippedDate = DateTime.UtcNow
        };
    }

    public void MarkAsDelivered(DateTime deliveredDate, string deliveryNotes = null)
    {
        Status = ShipmentStatus.Delivered;
        DeliveredDate = deliveredDate;
    }
}

public record ShipmentTrackingEvent(
    string Status,
    string Description,
    string Location = null)
{
    public DateTime EventDate { get; } = DateTime.UtcNow;
}