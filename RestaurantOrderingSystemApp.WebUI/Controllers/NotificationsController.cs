using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.NotificationDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class NotificationsController(INotificationService _notificationService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultNotificationDto>>(_notificationService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateNotification()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            Notification notification = new Notification()
            {
                Description = createNotificationDto.Description,
                IconType = createNotificationDto.IconType,
                Status = false,
                Date = DateTime.Now
            };
            _notificationService.TAdd(notification);

            if (notification.NotificationID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteNotification(int id)
        {
            var value = _notificationService.TGetByID(id);
            _notificationService.TDelete(value);
            value = _notificationService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateNotification(int id)
        {
            var value = _mapper.Map<UpdateNotificationDto>(_notificationService.TGetByID(id));
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            Notification notification = new Notification()
            {
                NotificationID = updateNotificationDto.NotificationID,
                Description = updateNotificationDto.Description,
                IconType = updateNotificationDto.IconType,
                Status = updateNotificationDto.Status,
                Date = DateTime.Now,
            };
            _notificationService.TUpdate(notification);

            if (notification.NotificationID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult NotificationStatusChange(int id)
        {
            _notificationService.TNotificationStatusChange(id);
            return RedirectToAction("Index");
        }

        public IActionResult AllNotificationStatusesChangeToTrue()
        {
            _notificationService.TAllNotificationStatusesChangeToTrue();
            return RedirectToAction("Index");
        }
    }
}
