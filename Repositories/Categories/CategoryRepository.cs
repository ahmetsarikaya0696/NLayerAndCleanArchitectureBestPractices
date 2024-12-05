using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext appDbContext) : GenericRepository<Category>(appDbContext), ICategoryRepository
    {
        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            var category = await _dbSet.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

            return category;
        }

        public IQueryable<Category?> GetCategoriesWithProducts()
        {
            var category = _dbSet.Include(x => x.Products).AsQueryable();

            return category;
        }
    }
}
