using AutoMapper;
using RestaurantOrderingSystemApp.WebUI.Dtos.TestimonialDtos;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.WebUI.Mapping
{
    public class TestimonialMapping : Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialDto>().ReverseMap();

        }
    }
}
