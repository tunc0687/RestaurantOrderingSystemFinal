using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDetailDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class OrderDetailController(IOrderService _orderService, IOrderDetailService _orderDetailService, IMapper _mapper) : Controller
    {
        [HttpGet]
        public IActionResult GetOrderDetail(int id)
        {
            var valuesOrderDetail = _mapper.Map<List<ResultOrderDetailWithProductDto>>(_orderDetailService.TGetOrderDetailsWithProducts(id));
            if (valuesOrderDetail != null)
            {
                var valueOrder = _mapper.Map<ResultOrderWithMenuTableNameDto>(_orderService.TFindOrderWithMenuTableName(id));
                return View(Tuple.Create(valuesOrderDetail, valueOrder));
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangeDescription(int id, int orderId)
        {
            _orderDetailService.TChangeDescription(id);
            return Redirect("/OrderDetail/GetOrderDetail/" + orderId);
        }
    }
}
