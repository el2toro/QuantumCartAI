namespace BuildingBlocks.Messaging.Events;

public record PaymentSucceededEvent(Guid OrderId, Guid CustomerId, decimal Amount, string PaymentMethod);
