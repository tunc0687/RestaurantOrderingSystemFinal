using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.CouponDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class CouponMapping : Profile
    {
        public CouponMapping()
        {
            CreateMap<Coupon, ResultCouponDto>().ReverseMap();
            CreateMap<Coupon, CreateCouponDto>().ReverseMap();
            CreateMap<Coupon, GetCouponDto>().ReverseMap();
            CreateMap<Coupon, UpdateCouponDto>().ReverseMap();
        }
    }
}
