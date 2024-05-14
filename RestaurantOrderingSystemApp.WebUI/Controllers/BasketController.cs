using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.BasketDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.OrderDtos;
using System.Text;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class BasketController(IBasketService _basketService, IMapper _mapper, IOrderService _orderService, IOrderDetailService _orderDetailService, INotificationService _notificationService) : Controller
    {
        public IActionResult Index()
        {
            var tableId = HttpContext.Session.GetInt32("MenuTableId");
            var code = HttpContext.Session.GetString("CustomerCode");
            if (tableId == null)
            {
                return RedirectToAction("Index", "Default");
            }

            var values = _mapper.Map<List<ResultBasketWithProductDto>>(_basketService.TGetBasketByCustomerCodeWithProductName(code));

            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public IActionResult CompleteOrder(CompleteOrderDto completeOrderDto)
        {
            completeOrderDto.Status = true;

            // Order Transactions
            var valueOrder = _mapper.Map<ResultOrderWithMenuTableNameDto>(_orderService.TFindOrderByMenuTableId(completeOrderDto.MenuTableID));
            if (valueOrder == null)
            {
                var createOrder = new Order()
                {
                    OrderDate = completeOrderDto.OrderDate,
                    TotalPrice = completeOrderDto.TotalPrice,
                    TotalDiscount = completeOrderDto.TotalDiscount,
                    FinalPrice = completeOrderDto.FinalPrice,
                    Status = completeOrderDto.Status,
                    MenuTableID = completeOrderDto.MenuTableID,
                };
                _orderService.TAdd(createOrder);
                if (createOrder.OrderID <= 0)
                {
                    return View();
                }
            }
            else
            {
                var updateOrder = new Order()
                {
                    OrderID = valueOrder.OrderID,
                    OrderDate = valueOrder.OrderDate,
                    TotalPrice = valueOrder.TotalPrice + completeOrderDto.TotalPrice,
                    TotalDiscount = valueOrder.TotalDiscount + completeOrderDto.TotalDiscount,
                    FinalPrice = valueOrder.FinalPrice + completeOrderDto.FinalPrice,
                    Status = valueOrder.Status,
                    MenuTableID = valueOrder.MenuTableID
                };
                _orderService.TUpdate(updateOrder);
                if (updateOrder.OrderID <= 0)
                {
                    return View();
                }
            }


            // Order Detail Transactions
            var getOrder = _mapper.Map<ResultOrderWithMenuTableNameDto>(_orderService.TFindOrderByMenuTableId(completeOrderDto.MenuTableID));

            var valuesBasket = _mapper.Map<List<ResultBasketWithProductDto>>(_basketService.TGetBasketByCustomerCodeWithProductName(completeOrderDto.CustomerCode));
            if (valuesBasket != null)
            {
                foreach (var value in valuesBasket)
                {
                    var createOrderDetail = new OrderDetail()
                    {
                        Count = value.Count,
                        TotalPrice = value.TotalPrice,
                        Description = "Sipariş Alındı",
                        CustomerCode = value.CustomerCode,
                        ProductID = value.ProductID,
                        OrderID = getOrder.OrderID
                    };
                    _orderDetailService.TAdd(createOrderDetail);
                    if (createOrderDetail.OrderDetailID <= 0)
                    {
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }

            // Basket DeleteAll Transactions
            _basketService.TDeleteBasketByCustomerCode(completeOrderDto.CustomerCode);
            var valuesDeleteBasket = _basketService.TFindList(x => x.CustomerCode == completeOrderDto.CustomerCode);

            if (valuesDeleteBasket != null)
            {
                return View();
            }

            // Notification Transactions
            var createNotification = new Notification()
            {
                IconType = "Sipariş",
                Description = $"{getOrder.MenuTableName} masasına yeni sipariş verildi.",
                Date = DateTime.Now,
                Status = false
            };
            _notificationService.TAdd(createNotification);
            if (createNotification.NotificationID > 0)
            {
                decimal discount = Convert.ToDecimal(HttpContext.Session.GetString("TotalDiscount"));
                HttpContext.Session.SetString("TotalDiscount", $"{completeOrderDto.TotalDiscount + discount}");
                Thread.Sleep(3000);
                return RedirectToAction("Index", "OrderUI");
            }

            return View();
        }

        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetByID(id);
            _basketService.TDelete(value);
            value = _basketService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return NoContent();
        }
    }
}
