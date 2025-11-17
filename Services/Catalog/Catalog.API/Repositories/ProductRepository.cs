using Catalog.API.Interfaces;

namespace Catalog.API.Repositories;

public class ProductRepository(ProductDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken)
    {
        var crearedProduct = dbContext.Products.Add(product).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);

        return crearedProduct;
    }

    public async Task DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        var existingProduct = await dbContext.Products.FindAsync(productId, cancellationToken);
        dbContext.Products.Remove(existingProduct);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products.FindAsync(productId, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
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
