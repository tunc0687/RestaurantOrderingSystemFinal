using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.CategoryDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class CategoryController(ICategoryService _categoryService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category()
            {
                CategoryName = createCategoryDto.CategoryName,
                Status = true,
            };
            _categoryService.TAdd(category);
            if (category.CategoryID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            value = _categoryService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var category = new Category()
            {
                CategoryName = updateCategoryDto.CategoryName,
                CategoryID = updateCategoryDto.CategoryID,
                Status = updateCategoryDto.Status,
            };
            _categoryService.TUpdate(category);
            if (category.CategoryID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
