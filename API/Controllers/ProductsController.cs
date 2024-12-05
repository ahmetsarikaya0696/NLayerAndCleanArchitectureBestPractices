using Application.Features.Products;
using Application.Features.Products.Create;
using Application.Features.Products.Update;
using Application.Features.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithCategoryAsync()
        {
            var serviceResult = await productService.GetAllWithCategoryAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var serviceResult = await productService.GetPaginatedAsync(pageNumber, pageSize);
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdWithCategoryAsync(int id)
        {
            var serviceResult = await productService.GetByIdWithCategoryAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductRequest createProductRequest)
        {
            var serviceResult = await productService.CreateAsync(createProductRequest);
            return CreateActionResult(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            var serviceResult = await productService.UpdateAsync(updateProductRequest);
            return CreateActionResult(serviceResult);
        }

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest)
        {
            var serviceResult = await productService.UpdateStockAsync(updateProductStockRequest);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
