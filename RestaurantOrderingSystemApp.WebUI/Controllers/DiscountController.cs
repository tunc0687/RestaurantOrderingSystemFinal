using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.DiscountDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class DiscountController(IDiscountService _discountService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto, IFormFile formFile)
        {
            createDiscountDto.ImageUrl = FileService.Upload(formFile);
            var discount = new Discount()
            {
                Amount = createDiscountDto.Amount,
                Description = createDiscountDto.Description,
                ImageUrl = createDiscountDto.ImageUrl,
                Title = createDiscountDto.Title,
                Status = false,
            };
            _discountService.TAdd(discount);
            if (discount.DiscountID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            _discountService.TDelete(value);
            value = _discountService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                updateDiscountDto.ImageUrl = FileService.Upload(formFile);
            }

            var discount = new Discount()
            {
                Amount = updateDiscountDto.Amount,
                Description = updateDiscountDto.Description,
                ImageUrl = updateDiscountDto.ImageUrl,
                Title = updateDiscountDto.Title,
                DiscountID = updateDiscountDto.DiscountID,
                Status = false,
            };
            _discountService.TUpdate(discount);
            if (discount.DiscountID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            _discountService.TChangeStatus(id);
            return RedirectToAction("Index");
        }
    }
}
