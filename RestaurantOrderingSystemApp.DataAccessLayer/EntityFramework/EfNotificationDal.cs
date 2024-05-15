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
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfNotificationDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void AllNotificationStatusesChangeToTrue()
        {
            var values = _context.Notifications.Where(x => x.Status == false).ToList();
            foreach (var value in values)
            {
                value.Status = true;
            }
            _context.SaveChanges();
        }

        public List<Notification> GetAllNotificationByFalse()
        {
            return _context.Notifications.Where(x=>x.Status==false).ToList();
        }

        public int NotificationCountByStatusFalse()
        {
            return _context.Notifications.Where(x=>x.Status==false).Count(); 
        }

        public void NotificationStatusChange(int id)
        {
            var value = _context.Notifications.Find(id);
            if (value != null)
            {
                value.Status = !value.Status;
                _context.SaveChanges();
            }
        }
    }
}
