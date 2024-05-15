using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.TestimonialDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultTestimonialComponentPartial(ITestimonialService _testimonialService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = _mapper.Map<List<ResultTestimonialDto>>(_testimonialService.TGetTestimonialsByStatusTrue());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}

