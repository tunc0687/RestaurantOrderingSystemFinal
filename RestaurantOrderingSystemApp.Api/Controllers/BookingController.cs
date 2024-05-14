﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.DtoLayer.BookingDto;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using System.Net.Mail;

namespace RestaurantOrderingSystemApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();
            return Ok(values);
        }

        [HttpGet("GetBookingListByStatus")]
        public IActionResult GetBookingListByDescription(string description)
        {
            var values = _bookingService.TFindList(x => x.Description == description);
            return Ok(values);
        }

        [HttpGet("TotalBookingCount")]
        public ActionResult TotalBookingCount()
        {
            return Ok(_bookingService.TTotalBookingCount());
        }

        [HttpPost]

        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
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
            return Ok("Rezervasyon Yapıldı");
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBooking(int id)
        {

            var value = _bookingService.TGetByID(id);
            _bookingService.TDelete(value);
            return Ok("Rezervasyon Silindi");
        }

        [HttpPut]

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
            return Ok("Rezervasyon Güncellendi");

        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon Durumu Değiştirildi");
        }

        [HttpGet("BookingStatusCancelled/{id}")]
        public IActionResult BookingStatusCancelled(int id)
        {
            _bookingService.TBookingStatusCancelled(id);
            return Ok("Rezervasyon Durumu Değiştirildi");
        }
    }

}
