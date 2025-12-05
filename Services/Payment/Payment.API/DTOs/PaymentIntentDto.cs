namespace Payment.API.DTOs;

public record PaymentIntentDto(List<PaymentIntentItemDto> Items);
public record PaymentIntentItemDto(int Quantity, PriceData PriceData);
public record PriceData(string Currency, ProductData ProductData, long UnitAmount);
public record ProductData(string Name, List<string> Images);
