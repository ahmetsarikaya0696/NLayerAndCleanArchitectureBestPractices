using Application.Features.Products.Create;
using Application.Features.Products.Dto;
using Application.Features.Products.Update;
using Application.Features.Products.UpdateStock;

namespace Application.Features.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult<List<ProductDto>>> GetAllWithCategoryAsync();
        ServiceResult<List<ProductDto>> GetPaginated(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDto?>> GetByIdWithCategoryAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest createProductRequest);
        Task<ServiceResult> UpdateAsync(UpdateProductRequest updateProductRequest);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
