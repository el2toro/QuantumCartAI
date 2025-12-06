using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;
using SharedKernel.Common;

public class OrderStatusHistory : AggregateRoot<OrderStatusHistoryId>
{
    public OrderId OrderId { get; private set; }
    public OrderStatus Status { get; private set; }
    public string ChangedBy { get; private set; } // "system", "customer", "admin", etc.
    public string Notes { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public Dictionary<string, object> Metadata { get; private set; } = new();

    // Navigation property (not used in EF, but for domain logic)
    public Order Order { get; private set; }

    private OrderStatusHistory() { } // For EF Core

    public OrderStatusHistory(
        OrderId orderId,
        OrderStatus status,
        string changedBy,
        string notes = null,
        Dictionary<string, object> metadata = null)
    {
        Id = OrderStatusHistoryId.Create();
        OrderId = orderId;
        Status = status;
        ChangedBy = changedBy ?? throw new ArgumentNullException(nameof(changedBy));
        Notes = notes;
        ChangedAt = DateTime.UtcNow;
        Metadata = metadata ?? new Dictionary<string, object>();
    }

    // Domain methods
    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }

    public void AddMetadata(string key, object value)
    {
        Metadata[key] = value;
    }
}

public record OrderStatusHistoryId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static OrderStatusHistoryId Of(Guid value) => new(value);
    public static OrderStatusHistoryId Create() => new(Guid.NewGuid());
}