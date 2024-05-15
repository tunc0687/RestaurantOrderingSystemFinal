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
    public class EfMoneyCaseDal : GenericRepository<MoneyCase>, IMoneyCaseDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfMoneyCaseDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void AddToMoneyCase(decimal price)
        {
            var value = _context.MoneyCases.FirstOrDefault();
            if (value != null)
            {
                value.TotalAmount += price;
            }
            _context.SaveChanges();
        }

        public decimal TotalMoneyCaseAmount()
        {
            return _context.MoneyCases.Select(x => x.TotalAmount).FirstOrDefault();
        }
    }
}
