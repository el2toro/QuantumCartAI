using BuildingBlocks.CQRS;
using Catalog.API.DTOs;
using Catalog.API.Interfaces;
using System.Collections.ObjectModel;

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
public class CreateProductHandler(IProductRepository productRepository)
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
                productToBeCreated.ProductCategories.Add(new ProductCategory { CategoryId = categoryId });
            }
        }

        var createdProduct = await productRepository.CreateProduct(productToBeCreated, cancellationToken);
        var result = createdProduct.Adapt<ProductDto>();

        return new CreateProductResult(result);
    }
}
