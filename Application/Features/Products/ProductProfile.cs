using Application.Features.Products.Create;
using Application.Features.Products.Dto;
using Application.Features.Products.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name.ToLowerInvariant()));

            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
