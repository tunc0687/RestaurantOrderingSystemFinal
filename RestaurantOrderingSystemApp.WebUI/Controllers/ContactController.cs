using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using RestaurantOrderingSystemApp.WebUI.Dtos.ContactDtos;

namespace RestaurantOrderingSystemApp.WebUI.Controllers
{
    public class ContactController(IContactService _contactService, IMapper _mapper) : Controller
    {
        public IActionResult Index()
        {
            var values = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            var contact = new Contact()
            {
                FooterDescription = createContactDto.FooterDescription,
                Location = createContactDto.Location,
                Mail = createContactDto.Mail,
                Phone = createContactDto.Phone,
                FooterTitle = createContactDto.FooterTitle,
                OpenDays = createContactDto.OpenDays,
                OpenDaysDescription = createContactDto.OpenDaysDescription,
                OpenHours = createContactDto.OpenHours
            };
            _contactService.TAdd(contact);
            if (contact.ContactID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteContact(int id)
        {
            var value = _contactService.TGetByID(id);
            _contactService.TDelete(value);
            value = _contactService.TGetByID(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value = _contactService.TGetByID(id);
            if (value != null)
            {
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            var contact = new Contact()
            {
                ContactID = updateContactDto.ContactID,
                FooterDescription = updateContactDto.FooterDescription,
                Location = updateContactDto.Location,
                Mail = updateContactDto.Mail,
                Phone = updateContactDto.Phone,
                FooterTitle = updateContactDto.FooterTitle,
                OpenHours = updateContactDto.OpenHours,
                OpenDays = updateContactDto.OpenDays,
                OpenDaysDescription = updateContactDto.OpenDaysDescription
            };
            _contactService.TUpdate(contact);
            if (contact.ContactID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
