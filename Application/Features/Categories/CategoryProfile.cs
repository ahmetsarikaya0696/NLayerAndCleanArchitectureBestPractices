using Application.Features.Categories.Create;
using Application.Features.Categories.Dto;
using Application.Features.Categories.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<Category, CreateCategoryResponse>();

            CreateMap<Category, CategoryWithProductsDto>();
        }
    }
}
