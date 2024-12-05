using Application.Features.Categories.Create;
using Application.Features.Categories.Dto;
using Application.Features.Categories.Update;

namespace Application.Features.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest createCategoryRequest);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
        Task<ServiceResult> DeleteAsync(int id);
        ServiceResult<List<CategoryDto>> GetAll();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryWithProductsDto>> GetWithProductsAsync(int id);
        ServiceResult<List<CategoryWithProductsDto>> GetAllWithProducts();
    }
}
