using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.CouponDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class CouponController(ICouponService _couponService) : Controller
    {
        public IActionResult Index()
        {
            var values = _couponService.TGetListAll();
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCoupon(CreateCouponDto createCouponDto)
        {
            Coupon coupon = new Coupon()
            {
                CouponCode = createCouponDto.CouponCode,
                Amount = createCouponDto.Amount,
                Status = true
            };

            _couponService.TAdd(coupon);
            if (coupon.CouponID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteCoupon(int id)
        {
            var value = _couponService.TGetByID(id);
            _couponService.TDelete(value);
            value = _couponService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCoupon(int id)
        {
            var value = _couponService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCoupon(UpdateCouponDto updateCouponDto)
        {
            updateCouponDto.Status = true;
            Coupon coupon = new Coupon()
            {
                CouponID = updateCouponDto.CouponID,
                CouponCode = updateCouponDto.CouponCode,
                Amount = updateCouponDto.Amount,
                Status = updateCouponDto.Status,
            };
            _couponService.TUpdate(coupon);
            if (coupon.CouponID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            _couponService.TChangeStatus(id);
            return RedirectToAction("Index");
        }
    }
}
