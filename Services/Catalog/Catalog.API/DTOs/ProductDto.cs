namespace Catalog.API.DTOs;

public record ProductDto(Guid Id,
    string Name,
    string Description,
    string Sku,
    string ImageFile,
    decimal Price,
    int Rating,
    bool IsAvailable,
    IEnumerable<Guid> CategoriesId);

