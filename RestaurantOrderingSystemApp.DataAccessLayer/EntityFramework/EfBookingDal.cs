using RestaurantOrderingSystemApp.DataAccessLayer.Abstract;
using RestaurantOrderingSystemApp.DataAccessLayer.Concrete;
using RestaurantOrderingSystemApp.DataAccessLayer.Repositories;
using RestaurantOrderingSystemApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderingSystemApp.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfBookingDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void BookingStatusApproved(int id)
        {
            var result = _context.Bookings.Find(id);
            result.Description = "Rezervasyon Onaylandı";
            _context.SaveChanges();
        }

        public void BookingStatusCancelled(int id)
        {
            var result = _context.Bookings.Find(id);
            result.Description = "Rezervasyon İptal Edildi";
            _context.SaveChanges();
        }

        public int TotalBookingCount()
        {
            return _context.Bookings
                .Count();
        }
    }
}
