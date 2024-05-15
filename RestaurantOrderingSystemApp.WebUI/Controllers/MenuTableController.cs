using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.MenuTableDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class MenuTableController(IMenuTableService _menuTableService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultMenuTableDto>>(_menuTableService.TGetListAll());
            if (values != null)
            {
                List<string> menuTablesQrCodeImages = new List<string>();
                List<string> encodeMenuTableIdList = new List<string>();
                foreach (var value in values)
                {
                    string encodeUrl = "https://bkrestoran.online/Menu/Index?mtCode=" + AesOperation.Encode($"{value.MenuTableID}");
                    menuTablesQrCodeImages.Add(QRCodeService.GenerateQrCode(encodeUrl));
                    encodeMenuTableIdList.Add(encodeUrl);
                }
                return View(Tuple.Create(values, encodeMenuTableIdList, menuTablesQrCodeImages));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateMenuTable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMenuTable(CreateMenuTableDto createMenuTableDto)
        {
            createMenuTableDto.Status = false;

            MenuTable menuTable = new MenuTable()
            {
                Name = createMenuTableDto.Name,
                Status = createMenuTableDto.Status,
            };

            _menuTableService.TAdd(menuTable);
            if (menuTable.MenuTableID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteMenuTable(int id)
        {
            var value = _menuTableService.TGetByID(id);
            _menuTableService.TDelete(value);
            value = _menuTableService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateMenuTable(int id)
        {
            var value = _mapper.Map<UpdateMenuTableDto>(_menuTableService.TGetByID(id));
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateMenuTable(UpdateMenuTableDto updateMenuTableDto)
        {
            updateMenuTableDto.Status = false;

            MenuTable menuTable = new MenuTable()
            {
                MenuTableID = updateMenuTableDto.MenuTableID,
                Name = updateMenuTableDto.Name,
                Status = updateMenuTableDto.Status
            };
            _menuTableService.TUpdate(menuTable);

            if (menuTable.MenuTableID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult TableListByStatus()
        {
            var values = _mapper.Map<List<ResultMenuTableDto>>(_menuTableService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }
}
