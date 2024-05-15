using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.CategoryDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.ProductDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOurMenuComponentPartial(IProductService _productService, ICategoryService _categoryService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            var valuesProduct = _mapper.Map<List<ResultProductWithCategory>>(_productService.TGetProductsWithCategories());
            var valuesCategory = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            if (valuesProduct != null & valuesCategory != null)
            {
                ViewData["Category"] = valuesCategory?.ToList();
                return View(valuesProduct);
            }
            return View();
        }

    }
}

