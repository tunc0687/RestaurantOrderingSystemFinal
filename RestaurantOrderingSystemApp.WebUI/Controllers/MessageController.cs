using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.MessageDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class MessageController(IMessageService _messageService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultMessageDto>>(_messageService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _messageService.TGetByID(id);
            _messageService.TDelete(value);
            value = _messageService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var value = _mapper.Map<UpdateMessageDto>(_messageService.TGetByID(id));
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            updateMessageDto.MessageSendDate = DateTime.Now;

            Message message = new Message()
            {
                Mail = updateMessageDto.Mail,
                MessageContent = updateMessageDto.MessageContent,
                MessageSendDate = updateMessageDto.MessageSendDate,
                NameSurname = updateMessageDto.NameSurname,
                Phone = updateMessageDto.Phone,
                Status = false,
                Subject = updateMessageDto.Subject,
                MessageID = updateMessageDto.MessageID,
            };
            _messageService.TUpdate(message);

            if (message.MessageID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult MessageStatusChange(int id)
        {
            _messageService.TMessageStatusChange(id);
            return RedirectToAction("Index");
        }

        public IActionResult AllMessageStatusesChangeToTrue()
        {
            _messageService.TAllMessageStatusesChangeToTrue();
            return RedirectToAction("Index");
        }
    }
}
