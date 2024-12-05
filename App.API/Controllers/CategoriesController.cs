using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var serviceResult = await categoryService.GetAllAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("products-included")]
        public async Task<IActionResult> GetAllWithProductsAsync()
        {
            var serviceResult = await categoryService.GetAllWithProductsAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var serviceResult = await categoryService.GetByIdAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}/products-included")]
        public async Task<IActionResult> GetWithProductsAsync(int id)
        {
            var serviceResult = await categoryService.GetWithProductsAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryRequest createCategoryRequest)
        {
            var serviceResult = await categoryService.CreateAsync(createCategoryRequest);
            return CreateActionResult(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            var serviceResult = await categoryService.UpdateAsync(updateCategoryRequest);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var serviceResult = await categoryService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
