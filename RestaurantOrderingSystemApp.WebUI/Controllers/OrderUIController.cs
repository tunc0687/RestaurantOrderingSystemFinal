using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDetailDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class OrderUIController(IOrderDetailService _orderDetailService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var tableId = HttpContext.Session.GetInt32("MenuTableId");
            var code = HttpContext.Session.GetString("CustomerCode");
            if (tableId == null)
            {
                return RedirectToAction("Index", "Default");
            }

            var values = _mapper.Map<List<ResultOrderDetailWithProductDto>>(_orderDetailService.TGetOrderDetailsByCustomerCodeWithProducts(code));
            if (values != null)
            {
                return View(values);
            }
            return View(new List<ResultOrderDetailWithProductDto>());
        }
    }
}
