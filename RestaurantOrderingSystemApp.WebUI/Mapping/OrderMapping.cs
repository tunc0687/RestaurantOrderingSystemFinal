using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class OrderMapping :Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, ResultOrderWithMenuTableNameDto>()
                .ForMember(e => e.MenuTableName, o => o.MapFrom(d => d.MenuTable.Name))
                .ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, GetOrderDto>().ReverseMap();
        }
    }
}
