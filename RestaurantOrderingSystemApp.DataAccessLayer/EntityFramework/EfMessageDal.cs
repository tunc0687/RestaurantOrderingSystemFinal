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
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfMessageDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void AllMessageStatusesChangeToTrue()
        {
            var values = _context.Messages.Where(x => x.Status == false).ToList();
            foreach (var value in values)
            {
                value.Status = true;
            }
            _context.SaveChanges();
        }

        public List<Message> GetAllMessageByFalse()
        {
            return _context.Messages.Where(x => x.Status == false).ToList();
        }

        public int MessageCountByStatusFalse()
        {
            return _context.Messages.Where(x => x.Status == false).Count();
        }

        public void MessageStatusChange(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                value.Status = !value.Status;
                _context.SaveChanges();
            }
        }
    }
}
