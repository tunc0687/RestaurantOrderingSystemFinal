using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.CategoryDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.ProductDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class ProductController(IProductService _productService, ICategoryService _categoryService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultProductWithCategory>>(_productService.TGetProductsWithCategories());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var values = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            List<SelectListItem> values2 = (from x in values
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryID.ToString()
                                            }).ToList();
            ViewBag.v = values2;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto, IFormFile formFile)
        {
            createProductDto.ImageUrl = FileService.Upload(formFile);
            createProductDto.ProductStatus = true;

            var product = new Product()
            {
                Description = createProductDto.Description,
                ImageUrl = createProductDto.ImageUrl,
                Price = createProductDto.Price,
                ProductName = createProductDto.ProductName,
                ProductStatus = createProductDto.ProductStatus,
                CategoryID = createProductDto.CategoryID
            };
            _productService.TAdd(product);
            if (product.ProductID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetByID(id);
            _productService.TDelete(value);
            value = _productService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var values1 = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            List<SelectListItem> values2 = (from x in values1
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryID.ToString()
                                            }).ToList();
            ViewBag.v = values2;

            var value = _productService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                updateProductDto.ImageUrl = FileService.Upload(formFile);
            }

            var product = new Product()
            {
                Description = updateProductDto.Description,
                ImageUrl = updateProductDto.ImageUrl,
                Price = updateProductDto.Price,
                ProductName = updateProductDto.ProductName,
                ProductStatus = updateProductDto.ProductStatus,
                ProductID = updateProductDto.ProductID,
                CategoryID = updateProductDto.CategoryID
            };
            _productService.TUpdate(product);
            if (product.ProductID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
