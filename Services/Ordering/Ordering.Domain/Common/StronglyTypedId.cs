//namespace Ordering.Domain.Common;

//public abstract record StronglyTypedId<TValue>(TValue Value)
//    where TValue : notnull
//{
//    public override string ToString() => Value.ToString();

//    // For JSON serialization
//    public virtual object GetValue() => Value;
//}

//// Strongly-typed IDs for Ordering domain
//public record OrderId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static OrderId Of(Guid value) => new(value);
//    public static OrderId Create() => new(Guid.NewGuid());
//}

//public record CustomerId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static CustomerId Of(Guid value) => new(value);
//    public static CustomerId Create() => new(Guid.NewGuid());
//}

//public record ProductId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static ProductId Of(Guid value) => new(value);
//}

//public record OrderItemId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static OrderItemId Of(Guid value) => new(value);
//    public static OrderItemId Create() => new(Guid.NewGuid());
//}

//public record PaymentId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static PaymentId Of(Guid value) => new(value);
//    public static PaymentId Create() => new(Guid.NewGuid());
//}

//public record ShipmentId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static ShipmentId Of(Guid value) => new(value);
//    public static ShipmentId Create() => new(Guid.NewGuid());
//}

//public record InvoiceId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static InvoiceId Of(Guid value) => new(value);
//    public static InvoiceId Create() => new(Guid.NewGuid());
//}

//public record OrderStatusHistoryId(Guid Value) : StronglyTypedId<Guid>(Value)
//{
//    public static OrderStatusHistoryId Of(Guid value) => new(value);
//    public static OrderStatusHistoryId Create() => new(Guid.NewGuid());
//}
