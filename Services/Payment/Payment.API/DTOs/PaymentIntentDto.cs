namespace Payment.API.DTOs;

public record PaymentIntentDto(long Amount, string Currency, Guid CustomerId, Guid OrderId);
