using AutoMapper;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.NotificationDtos;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            CreateMap<Notification, ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();

        }
    }
}
