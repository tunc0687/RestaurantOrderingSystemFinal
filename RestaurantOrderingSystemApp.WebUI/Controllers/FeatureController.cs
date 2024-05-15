using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.FeatureDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class FeatureController(IFeatureService _featureService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultFeatureDto>>(_featureService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto, IFormFile formFile)
        {
            createFeatureDto.Status = true;
            createFeatureDto.ImageUrl = FileService.Upload(formFile);

            var feature = new Feature()
            {
                Title = createFeatureDto.Title,
                Description = createFeatureDto.Description,
                ImageUrl = createFeatureDto.ImageUrl,
                Status = createFeatureDto.Status,

            };
            _featureService.TAdd(feature);
            if (feature.FeatureID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteFeature(int id)
        {
            var value = _featureService.TGetByID(id);
            _featureService.TDelete(value);
            value = _featureService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateFeature(int id)
        {
            var value = _featureService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                updateFeatureDto.ImageUrl = FileService.Upload(formFile);
            }

            updateFeatureDto.Status = true;

            var feature = new Feature()
            {
                FeatureID = updateFeatureDto.FeatureID,
                Title = updateFeatureDto.Title,
                Description = updateFeatureDto.Description,
                ImageUrl = updateFeatureDto.ImageUrl,
                Status = updateFeatureDto.Status
            };
            _featureService.TUpdate(feature);

            if (feature.FeatureID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            _featureService.TChangeStatus(id);
            return RedirectToAction("Index");
        }
    }
}
