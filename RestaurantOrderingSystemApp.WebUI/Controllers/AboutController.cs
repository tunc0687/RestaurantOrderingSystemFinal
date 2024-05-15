using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.AboutDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class AboutController(IAboutService _aboutService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultAboutDto>>(_aboutService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAbout(CreateAboutDto createAboutDto, IFormFile formFile)
        {
            createAboutDto.ImageUrl = FileService.Upload(formFile);
            About about = new About()
            {
                Title = createAboutDto.Title,
                Description = createAboutDto.Description,
                ImageUrl = createAboutDto.ImageUrl,
            };

            _aboutService.TAdd(about);

            if (about.AboutID > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.TGetByID(id);
            _aboutService.TDelete(value);
            value = _aboutService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var value = _mapper.Map<UpdateAboutDto>(_aboutService.TGetByID(id));
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto, IFormFile formFile)
        {
            if (formFile != null)
            {
                updateAboutDto.ImageUrl = FileService.Upload(formFile);
            }

            About about = new About()
            {
                AboutID = updateAboutDto.AboutID,
                Title = updateAboutDto.Title,
                Description = updateAboutDto.Description,
                ImageUrl = updateAboutDto.ImageUrl,
            };
            _aboutService.TUpdate(about);

            if (about.AboutID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
