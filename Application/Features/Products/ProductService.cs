﻿using Application.Contracts.Persistence;
using Application.Features.Products.Create;
using Application.Features.Products.Dto;
using Application.Features.Products.Update;
using Application.Features.Products.UpdateStock;
using AutoMapper;
using Domain.Entities;
using System.Net;

namespace Application.Features.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>()
            {
                Data = productsAsDto
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllWithCategoryAsync()
        {
            var productsWithCategory = await productRepository.GetAllWithCategoryAsync();

            var productsAsDto = mapper.Map<List<ProductDto>>(productsWithCategory);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public ServiceResult<List<ProductDto>> GetPaginated(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var products = productRepository.GetAll().Skip(skip).Take(pageSize).ToList();

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdWithCategoryAsync(int id)
        {
            var product = await productRepository.GetByIdWithCategoryAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found!", HttpStatusCode.NotFound);
            }

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest createProductRequest)
        {
            var category = await categoryRepository.GetByIdAsync(createProductRequest.CategoryId);

            if (category is null)
            {
                return ServiceResult<CreateProductResponse>.Fail("Category not found!");
            }

            var sameProductNameExist = productRepository.Where(x => x.Name == createProductRequest.Name).Any();

            if (sameProductNameExist)
            {
                return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.");
            }

            var product = mapper.Map<Product>(createProductRequest);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            var category = await categoryRepository.GetByIdAsync(updateProductRequest.CategoryId);

            if (category is null)
            {
                return ServiceResult.Fail("Category not found!");
            }

            var product = await productRepository.GetByIdAsync(updateProductRequest.Id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found!", HttpStatusCode.NotFound);
            }

            var sameProductNameExist = productRepository.Where(x => x.Name == updateProductRequest.Name && x.Id != updateProductRequest.Id).Any();

            if (sameProductNameExist)
            {
                return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır.");
            }

            mapper.Map(updateProductRequest, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest)
        {
            var product = await productRepository.GetByIdAsync(updateProductStockRequest.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found!", HttpStatusCode.NotFound);
            }

            product.Stock = updateProductStockRequest.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found!", HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
