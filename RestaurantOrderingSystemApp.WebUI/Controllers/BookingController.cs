using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.BookingDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.MailDtos;
using RestaurantOrderingSystemApp.WebUI.Services;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class BookingController(IBookingService _bookingService) : Controller
    {
        public IActionResult Index()
        {
            var values = _bookingService.TGetListAll();
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetBookingListByStatusToIncoming()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
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
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            _bookingService.TDelete(value);
            value = _bookingService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            Booking booking = new Booking()
            {
                Mail = updateBookingDto.Mail,
                BookingID = updateBookingDto.BookingID,
                Name = updateBookingDto.Name,
                PersonCount = updateBookingDto.PersonCount,
                Phone = updateBookingDto.Phone,
                Date = updateBookingDto.Date,
                Description = updateBookingDto.Description,
            };
            _bookingService.TUpdate(booking);
            if (booking.BookingID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);

            var value = _bookingService.TGetByID(id);
            if (value != null)
            {

                CreateMailDto createMailDto = new CreateMailDto()
                {
                    ReceiverMail = value.Mail,
                    Subject = "Rezervasyon Onayı",
                    Body = $"Merhaba {value.Name}. {value.Date.ToString("d")} tarihi için " +
                    $"yapmış olduğunuz rezervasyonunuz onaylanmıştır. Yerinizi ayırdık. " +
                    $"Şimdiden iyi eğlenceler dilerim. " +
                    $"B | K Restoran"
                };

                EmailService.SendEmail(createMailDto);

            }

            return RedirectToAction("Index");
        }

        public IActionResult BookingStatusCancelled(int id)
        {
            _bookingService.TBookingStatusCancelled(id);

            var value = _bookingService.TGetByID(id);
            if (value != null)
            {
                CreateMailDto createMailDto = new CreateMailDto()
                {
                    ReceiverMail = value.Mail,
                    Subject = "Rezervasyon İptali",
                    Body = $"Merhaba {value.Name}. {value.Date.ToString("d")} tarihi için " +
                        $"yapmış olduğunuz rezervasyonunuz restoranımızdaki yoğunluk sebebiyle iptal edilmiştir. " +
                        $"Restoranımızı arayarak uygun bir zamanda rezervasyonunuzu oluşturabiliriz. İyi günler dilerim. " +
                        $"B | K Restoran"
                };

                EmailService.SendEmail(createMailDto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetBookingListByStatus(string description)
        {
            var values = _bookingService.TFindList(x => x.Description == description);
            if (values != null)
            {
                ViewBag.Description = description;
                return View(values);
            }
            return View();
        }
    }
}
