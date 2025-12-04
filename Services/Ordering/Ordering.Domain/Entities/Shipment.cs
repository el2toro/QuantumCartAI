using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Shipment : Entity
{
    public ShipmentId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public string TrackingNumber { get; private set; }
    public string Carrier { get; private set; }
    public string ServiceType { get; private set; }
    public decimal ShippingCost { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime ShippedDate { get; private set; }
    public DateTime? EstimatedDeliveryDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public string DeliveryNotes { get; private set; }
    public Address DeliveryAddress { get; private set; }

    private readonly List<ShipmentTrackingEvent> _trackingEvents = new();
    public IReadOnlyCollection<ShipmentTrackingEvent> TrackingEvents => _trackingEvents.AsReadOnly();

    private Shipment() { }

    public static Shipment Create(
        OrderId orderId,
        string trackingNumber,
        string carrier,
        decimal shippingCost,
        Address deliveryAddress,
        string serviceType = "Standard")
    {
        return new Shipment
        {
            Id = ShipmentId.New(),
            OrderId = orderId,
            TrackingNumber = trackingNumber,
            Carrier = carrier,
            ServiceType = serviceType,
            ShippingCost = shippingCost,
            Status = ShipmentStatus.LabelCreated,
            ShippedDate = DateTime.UtcNow,
            DeliveryAddress = deliveryAddress
        };
    }

    public void MarkAsInTransit()
    {
        Status = ShipmentStatus.InTransit;
        AddTrackingEvent("IN_TRANSIT", "Package is in transit");
    }

    public void MarkAsOutForDelivery()
    {
        Status = ShipmentStatus.OutForDelivery;
        AddTrackingEvent("OUT_FOR_DELIVERY", "Package is out for delivery");
    }

    public void MarkAsDelivered(DateTime deliveredDate, string deliveryNotes = null)
    {
        Status = ShipmentStatus.Delivered;
        DeliveredDate = deliveredDate;
        DeliveryNotes = deliveryNotes;
        AddTrackingEvent("DELIVERED", "Package delivered successfully");

        //AddDomainEvent(new ShipmentDeliveredEvent(Id, OrderId, deliveredDate));
    }

    public void MarkAsFailed(string reason)
    {
        Status = ShipmentStatus.Failed;
        AddTrackingEvent("DELIVERY_FAILED", $"Delivery failed: {reason}");
    }

    public void MarkAsReturned(string reason)
    {
        Status = ShipmentStatus.Returned;
        AddTrackingEvent("RETURNED", $"Package returned: {reason}");
    }

    public void MarkAsLost()
    {
        Status = ShipmentStatus.Lost;
        AddTrackingEvent("LOST", "Package marked as lost");
    }

    public void UpdateEstimatedDelivery(DateTime estimatedDelivery)
    {
        EstimatedDeliveryDate = estimatedDelivery;
    }

    public void AddTrackingEvent(string status, string description, string location = null)
    {
        var trackingEvent = new ShipmentTrackingEvent(status, description, location);
        _trackingEvents.Add(trackingEvent);
    }
}


public record ShipmentTrackingEvent(
    string Status,
    string Description,
    string Location = null)
{
    public DateTime EventDate { get; } = DateTime.UtcNow;
}