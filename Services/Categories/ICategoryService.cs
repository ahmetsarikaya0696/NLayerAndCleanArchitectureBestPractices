using App.Services.Categories.Create;
using App.Services.Categories.Update;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest createCategoryRequest);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryWithProductsDto>> GetWithProductsAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProductsAsync();
    }
}
