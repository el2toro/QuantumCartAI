namespace Catalog.API.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByIdAsync(Guid[] ProductIds, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    Task<Product> CreateProduct(Product product, CancellationToken cancellationToken);
    Task<Product> UpdateProduct(Product product, CancellationToken cancellationToken);
    Task DeleteProduct(Product product, CancellationToken cancellationToken);
    Task<bool> UpdateStock(Guid productId, int quantity, CancellationToken cancellationToken);
}
