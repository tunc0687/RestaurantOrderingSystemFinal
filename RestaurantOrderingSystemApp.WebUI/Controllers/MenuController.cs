using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.BasketDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class MenuController(IMenuTableService _menuTableService, IBasketService _basketService) : Controller
    {
        public IActionResult Index(string mtCode = "bkrestoran")
        {
            var tableId = HttpContext.Session.GetInt32("MenuTableId");
            var menuTableId = Convert.ToInt32(AesOperation.Decode(mtCode));


            var valueMenuTable = _menuTableService.TGetByID(menuTableId);

            if (valueMenuTable == null && tableId == null)
            {
                return RedirectToAction("Index", "Default");
            }
            else if (valueMenuTable == null && menuTableId != 0 && tableId != null)
            {
                _menuTableService.TChangeStatusClose(Convert.ToInt32(tableId));
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Default");
            }
            else if (valueMenuTable != null)
            {
                HttpContext.Session.SetInt32("MenuTableId", menuTableId);
                HttpContext.Session.SetString("CustomerCode", Guid.NewGuid().ToString().Substring(0, 8));
                _menuTableService.TChangeStatusOpen(Convert.ToInt32(tableId));
            }


            return View();
        }

        [HttpPost]
        public IActionResult AddBasket([FromBody] CreateBasketDto createBasketDto)
        {
            var basket = new Basket()
            {
                Count = createBasketDto.Count,
                TotalPrice = createBasketDto.TotalPrice,
                CustomerCode = createBasketDto.CustomerCode,
                ProductID = createBasketDto.ProductID,
                MenuTableID = createBasketDto.MenuTableID
            };
            _basketService.TAdd(basket);
            if (basket.BasketID > 0)
            {
                return RedirectToAction("Index");
            }
            return Json(createBasketDto);
        }
    }
}
