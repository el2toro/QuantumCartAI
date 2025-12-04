namespace Ordering.Application.DTOs;

public record ShipmentTrackingEventDto
{
    public DateTime EventDate { get; init; }
    public string Status { get; init; }
    public string Location { get; init; }
    public string Description { get; init; }
}