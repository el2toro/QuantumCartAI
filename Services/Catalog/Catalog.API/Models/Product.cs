namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Image { get; set; } = default!;
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
