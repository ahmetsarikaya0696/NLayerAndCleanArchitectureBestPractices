using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetTopPriceProductsAsync(int count);
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetByIdWithCategoryAsync(int id);
    }
}
