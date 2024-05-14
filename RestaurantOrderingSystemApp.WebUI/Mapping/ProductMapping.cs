using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.ProductDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategory>()
                .ForMember(e => e.CategoryName, o => o.MapFrom(d => d.Category.CategoryName))
                .ReverseMap();
        }
    }
}
