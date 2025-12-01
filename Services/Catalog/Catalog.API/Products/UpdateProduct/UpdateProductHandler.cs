namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;
public record UpdateProductResult(ProductDto Product);
public class UpdateProductHandler(IProductRepository productRepository)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var productToBeUpdated = await productRepository
            .GetProductById(command.Product.Id, cancellationToken)
            ?? throw new ProductNotFoundException(command.Product.Id.ToString());

        productToBeUpdated.Sku = command.Product.Sku;
        productToBeUpdated.Name = command.Product.Name;
        productToBeUpdated.Description = command.Product.Description;
        productToBeUpdated.Price = command.Product.Price;
        productToBeUpdated.ImageFile = command.Product.ImageFile;
        productToBeUpdated.IsAvailable = command.Product.IsAvailable;
        productToBeUpdated.Rating = command.Product.Rating;
        productToBeUpdated.UpdatedAt = DateTime.UtcNow;



        //productToBeUpdated.ProductCategories = command.Product.CategoriesId.Any()
        //    ? command.Product.CategoriesId
        //     .Select(id => new ProductCategory { CategoryId = id, ProductId = command.Product.Id })
        //     .ToList()
        //   : [];

        var updatedProduct = await productRepository.UpdateProduct(productToBeUpdated, cancellationToken);
        var result = updatedProduct.Adapt<ProductDto>();

        return new UpdateProductResult(result);
    }
}
