using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.ContactDtos;


namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.UILayoutComponents
{
    public class _UILayoutFooterComponentPartial(IContactService _contactService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var values = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
