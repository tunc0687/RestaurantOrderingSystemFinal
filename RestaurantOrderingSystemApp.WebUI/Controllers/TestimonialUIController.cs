using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.BasketDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.NotificationDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.TestimonialDtos;
using System.Text;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class TestimonialUIController(ITestimonialService _testimonialService, INotificationService _notificationService) : Controller
    {
        [HttpPost]
        public IActionResult Index(CreateTestimonialDto createTestimonialDto)
        {
            createTestimonialDto.Status = true;
            createTestimonialDto.ImageUrl = "images/user_testimonial.jpg";

            var testimonial = new Testimonial()
            {
                Comment = createTestimonialDto.Comment,
                ImageUrl = createTestimonialDto.ImageUrl,
                Name = createTestimonialDto.Name,
                Status = createTestimonialDto.Status,
                Title = createTestimonialDto.Title,
            };
            _testimonialService.TAdd(testimonial);
            if (testimonial.TestimonialID > 0)
            {
                // Notification Transactions
                var notification = new Notification()
                {
                    IconType = "Yorum",
                    Description = $"{createTestimonialDto.Name} restoranımız hakkında yorum yaptı.",
                    Date = DateTime.Now,
                    Status = false
                };
                _notificationService.TAdd(notification);
                if (notification.NotificationID > 0)
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
