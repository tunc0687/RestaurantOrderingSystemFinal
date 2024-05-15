using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.AboutDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultAboutComponentPartial(IAboutService _aboutService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = _mapper.Map<List<ResultAboutDto>>(_aboutService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
