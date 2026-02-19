namespace Payment.API.DTOs;

public record PaymentIntentDto(decimal Amount, string Currency, Guid CustomerId, Guid OrderId);
