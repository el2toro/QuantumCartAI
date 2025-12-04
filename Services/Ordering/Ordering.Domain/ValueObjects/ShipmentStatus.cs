namespace Ordering.Domain.ValueObjects;

public enum ShipmentStatus
{
    Pending = 1,        // Shipment not yet created
    LabelCreated = 2,   // Shipping label created
    InTransit = 3,      // Package in transit
    OutForDelivery = 4, // Package out for delivery
    Delivered = 5,      // Package delivered
    Failed = 6,         // Delivery failed
    Returned = 7,       // Package returned
    Lost = 8            // Package lost
}