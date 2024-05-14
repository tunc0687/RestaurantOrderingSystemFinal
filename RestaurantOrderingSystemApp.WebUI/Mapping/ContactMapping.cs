using AutoMapper;

using RestaurantOrderingSystemApp.WebUI.Dtos.ContactDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetContactDto>().ReverseMap();

        }
    }
}
