namespace Catalog.API.Repositories;

public class ProductRepository(ProductDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        var crearedProduct = dbContext.Products.Add(product).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);

        return crearedProduct;
    }

    public async Task DeleteProduct(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(p => p.ProductCategories.Any(category => category.CategoryId == categoryId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        var updatedProduct = dbContext.Products.Update(product).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);

        return updatedProduct;
    }
}
