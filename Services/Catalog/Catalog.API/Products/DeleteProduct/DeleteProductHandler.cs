namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
public class DeleteProductHandler(IProductRepository productRepository)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository
            .GetProductById(command.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException(command.ProductId.ToString());

        await productRepository.DeleteProduct(product, cancellationToken);

        return new DeleteProductResult(true);
    }
}
