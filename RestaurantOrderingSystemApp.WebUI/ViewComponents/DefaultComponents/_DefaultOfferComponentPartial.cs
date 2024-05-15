using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.DiscountDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOfferComponentPartial(IDiscountService _discountService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var values = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}

