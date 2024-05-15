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
    public class EfDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfDiscountDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeStatus(int id)
        {
            var result = _context.Discounts.Find(id);
            if (result != null)
            {
                result.Status = !result.Status;
                _context.SaveChanges();
            }
        }
    }
}
