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
    public class EfCouponDal : GenericRepository<Coupon>, ICouponDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfCouponDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeStatus(int id)
        {
            var result = _context.Coupons.Find(id);
            result.Status = !result.Status;
            _context.SaveChanges();
        }

        public int GetAmountByCouponCode(string couponCode)
        {
            var result = _context.Coupons
                .Where(x => x.CouponCode == couponCode)
                .Where(y => y.Status)
                .Select(z => z.Amount)
                .FirstOrDefault();
            return result;
        }
    }
}
