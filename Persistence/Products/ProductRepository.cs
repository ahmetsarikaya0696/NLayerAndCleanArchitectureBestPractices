using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Products
{
    public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product>(appDbContext), IProductRepository
    {
        public async Task<List<Product>> GetTopPriceProductsAsync(int count)
        {
            return await _dbSet.OrderByDescending(x => x.Price)
                               .Take(count)
                               .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _dbSet.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            return await _dbSet.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
