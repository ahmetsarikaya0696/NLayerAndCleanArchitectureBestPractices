using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;

namespace App.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult<List<ProductDto>>> GetAllWithCategoryAsync();
        Task<ServiceResult<List<ProductDto>>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDto?>> GetByIdWithCategoryAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest createProductRequest);
        Task<ServiceResult> UpdateAsync(UpdateProductRequest updateProductRequest);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
