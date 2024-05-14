using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.MenuTableDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class MenuTableMapping : Profile
    {
        public MenuTableMapping()
        {
            CreateMap<MenuTable, ResultMenuTableDto>().ReverseMap();
            CreateMap<MenuTable, CreateMenuTableDto>().ReverseMap();
            CreateMap<MenuTable, UpdateMenuTableDto>().ReverseMap();
        }
    }
}
