using Catalog.API.Repositories;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
       string Name,
        string Description,
        string Sku,
        string ImageFile,
        decimal Price,
        int Rating,
        List<Guid> Categories) : ICommand<CreateProductResult>;
public record CreateProductResult(ProductDto Product);
public class CreateProductHandler(IProductRepository productRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productToBeCreated = command.Adapt<Product>();
        productToBeCreated.CreatedAt = DateTime.UtcNow;
        productToBeCreated.UpdatedAt = DateTime.UtcNow;

        if (command.Categories.Any())
        {
            foreach (var categoryId in command.Categories)
            {
                var category = await categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);
                productToBeCreated.ProductCategories.Add(new ProductCategory { Category = category });
            }
        }

        var createdProduct = await productRepository.CreateProduct(productToBeCreated, cancellationToken);
        var result = createdProduct.Adapt<ProductDto>();

        return new CreateProductResult(result);
    }
}
