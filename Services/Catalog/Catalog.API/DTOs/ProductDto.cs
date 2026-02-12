namespace Catalog.API.DTOs;

public record ProductDto(Guid Id,
    string Name,
    string Description,
    string Sku,
    string Image,
    decimal Price,
    int Rating,
    bool IsAvailable,
    int Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<ProductCategory> ProductCategories);

