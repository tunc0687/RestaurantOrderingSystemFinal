using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.SocialMediaDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.UILayoutComponents
{
    public class _UISocialMediaComponentPartial(ISocialMediaService _socialMediaService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var values = _mapper.Map<List<ResultSocialMediaDto>>(_socialMediaService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
