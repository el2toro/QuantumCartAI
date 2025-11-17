namespace Catalog.API.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByCategory(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken);
    Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    Task<Product> CreateProduct(Product product, CancellationToken cancellationToken);
    Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken);
    Task DeleteProduct(Guid id, CancellationToken cancellationToken);
}
