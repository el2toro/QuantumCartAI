namespace Catalog.API.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Category> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
}

public class CategoryRepository(ProductDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        var createdCategory = dbContext.Categories.Add(category).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);

        return createdCategory;
    }

    public Task DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.FindAsync(categoryId, cancellationToken);
    }

    public Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
