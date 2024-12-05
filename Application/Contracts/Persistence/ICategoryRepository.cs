using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        IQueryable<Category?> GetCategoriesWithProducts();
    }
}
