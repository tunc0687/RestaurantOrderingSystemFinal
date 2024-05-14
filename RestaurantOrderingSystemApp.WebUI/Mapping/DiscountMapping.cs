using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.DiscountDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class DiscountMapping : Profile

    {
        public DiscountMapping()
        {
            CreateMap<Discount, ResultDiscountDto>().ReverseMap();
            CreateMap<Discount, UpdateDiscountDto>().ReverseMap();
            CreateMap<Discount, CreateDiscountDto>().ReverseMap();
            CreateMap<Discount, GetDiscountDto>().ReverseMap();

        }
    }
}
