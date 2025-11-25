namespace Catalog.API.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public IEnumerable<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
