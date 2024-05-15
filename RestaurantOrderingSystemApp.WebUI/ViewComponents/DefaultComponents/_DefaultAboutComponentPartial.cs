using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultAboutComponentPartial(IAboutService _aboutService) : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var values = _aboutService.TGetListAll();
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
