namespace Catalog.API.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    // Navigation property
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}

public class ProductCategory
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}
