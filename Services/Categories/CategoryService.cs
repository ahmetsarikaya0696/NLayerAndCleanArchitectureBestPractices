using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest createCategoryRequest)
        {
            var sameCategoryNameExist = await categoryRepository.Where(x => x.Name == createCategoryRequest.Name).AnyAsync();

            if (sameCategoryNameExist)
            {
                return ServiceResult<CreateCategoryResponse>.Fail("Kategori ismi veritabanında bulunmaktadır.");
            }

            var category = mapper.Map<Category>(createCategoryRequest);

            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();

            var createCategoryResponse = mapper.Map<CreateCategoryResponse>(category);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(createCategoryResponse, $"api/categories/{createCategoryResponse.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await categoryRepository.GetByIdAsync(updateCategoryRequest.Id);

            if (category is null)
            {
                return ServiceResult.Fail("Category not found!", HttpStatusCode.NotFound);
            }

            var sameCategoryNameExist = await categoryRepository.Where(x => x.Name == updateCategoryRequest.Name && x.Id != updateCategoryRequest.Id).AnyAsync();

            if (sameCategoryNameExist)
            {
                return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır.");
            }

            mapper.Map(updateCategoryRequest, category);

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult.Fail("Category not found!", HttpStatusCode.NotFound);
            }

            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found!", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetWithProductsAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found!", HttpStatusCode.NotFound);
            }

            var categoryWithProducts = await categoryRepository.GetCategoryWithProductsAsync(id);

            var categoryWithProductsDto = mapper.Map<CategoryWithProductsDto>(categoryWithProducts);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryWithProductsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProductsAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            if (categories is null || categories.Count == 0)
            {
                return ServiceResult<List<CategoryWithProductsDto>>.Fail("", HttpStatusCode.NotFound);
            }

            var categoriesWithProducts = await categoryRepository.GetCategoriesWithProducts().ToListAsync();

            var categoriesWithProductDto = mapper.Map<List<CategoryWithProductsDto>>(categoriesWithProducts);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesWithProductDto);
        }
    }
}
