using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.MessageDtos;
using RestaurantOrderingSystemApp.WebUI.Dtos.NotificationDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.LayoutComponents
{
    public class _LayoutNavbarComponentPartial(INotificationService _notificationService, IMapper _mapper, IMessageService _messageService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var notificationCountByFalse = _notificationService.TNotificationCountByStatusFalse();
            var messageCountByFalse = _messageService.TMessageCountByStatusFalse();

            var notificationListByFalse = _mapper.Map<List<ResultNotificationDto>>(_notificationService.TGetAllNotificationByFalse());
            var messageListByFalse = _mapper.Map<List<ResultMessageDto>>(_messageService.TGetAllMessageByFalse());

            if (notificationListByFalse != null)
            {
                ViewBag.NCount = notificationCountByFalse;
                ViewBag.MCount = messageCountByFalse;
                return View(Tuple.Create(notificationListByFalse, messageListByFalse));
            }
            return View();
        }
    }
}
