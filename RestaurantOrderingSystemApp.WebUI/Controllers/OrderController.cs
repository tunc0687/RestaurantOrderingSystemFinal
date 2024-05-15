using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class OrderController(IOrderService _orderService, IMoneyCaseService _moneyCaseService, IMenuTableService _menuTableService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultOrderWithMenuTableNameDto>>(_orderService.TGetOrdersWithMenuTableNames());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = new Order()
            {
                OrderDate = createOrderDto.OrderDate,
                TotalPrice = createOrderDto.TotalPrice,
                TotalDiscount = createOrderDto.TotalDiscount,
                FinalPrice = createOrderDto.FinalPrice,
                Status = true,
                MenuTableID = createOrderDto.MenuTableID,
            };
            _orderService.TAdd(order);
            if (order.OrderID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteOrder(int id)
        {
            var value = _orderService.TGetByID(id);
            _orderService.TDelete(value);
            value = _orderService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var order = new Order()
            {
                OrderID = updateOrderDto.OrderID,
                OrderDate = updateOrderDto.OrderDate,
                TotalPrice = updateOrderDto.TotalPrice,
                TotalDiscount = updateOrderDto.TotalDiscount,
                FinalPrice = updateOrderDto.FinalPrice,
                Status = updateOrderDto.Status,
                MenuTableID = updateOrderDto.MenuTableID,
            };
            _orderService.TUpdate(order);
            if (order.OrderID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CloseAccount(int id, int menuTableId, decimal price)
        {
            _orderService.TCloseAccount(id);
            _moneyCaseService.TAddToMoneyCase(price);
            _menuTableService.TChangeStatusClose(menuTableId);
            return RedirectToAction("Index", "Order");
        }
    }
}
