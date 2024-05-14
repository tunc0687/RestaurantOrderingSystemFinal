using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.BookingDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class BookATableController(IBookingService _bookingService, INotificationService _notificationService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateBookingDto createBookingDto)
        {
            createBookingDto.Description = "Rezervasyon Alındı";
            Booking booking = new Booking()
            {
                Mail = createBookingDto.Mail,
                Date = createBookingDto.Date,
                Name = createBookingDto.Name,
                PersonCount = createBookingDto.PersonCount,
                Phone = createBookingDto.Phone,
                Description = createBookingDto.Description,
            };

            _bookingService.TAdd(booking);
            if (booking.BookingID > 0)
            {
                // Notification Transactions
                var createNotification = new Notification()
                {
                    IconType = "Rezervasyon",
                    Description = $"{createBookingDto.Name} restoranımıza {createBookingDto.Date} tarihine {createBookingDto.PersonCount} kişilik rezervasyon oluşturdu.",
                    Date = DateTime.Now,
                    Status = false
                };
                _notificationService.TAdd(createNotification);
                if (createNotification.NotificationID > 0)
                {
                    Thread.Sleep(3000);
                    return RedirectToAction("Index", "Default");
                }
                return View();
            }
            return View();
        }
    }
}
