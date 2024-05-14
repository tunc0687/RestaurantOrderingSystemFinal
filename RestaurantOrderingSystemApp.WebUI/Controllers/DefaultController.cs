using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.MessageDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController(IMessageService _messageService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();

        }
        [HttpPost]
        public IActionResult SendMessage(CreateMessageDto createMessageDto)
        {
            Message message = new Message()
            {
                Mail = createMessageDto.Mail,
                MessageContent = createMessageDto.MessageContent,
                MessageSendDate = DateTime.Now,
                NameSurname = createMessageDto.NameSurname,
                Phone = createMessageDto.Phone,
                Status = false,
                Subject = createMessageDto.Subject,
            };

            _messageService.TAdd(message);
            if (message.MessageID > 0)
            {
                Thread.Sleep(3000);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
