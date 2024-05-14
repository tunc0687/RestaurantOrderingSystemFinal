using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.MessageDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class MessageMapping : Profile
    {
        public MessageMapping()
        {

            CreateMap<Message ,ResultMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();



        }

    }
}
