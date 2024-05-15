using Microsoft.EntityFrameworkCore;
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
    public class EfBasketDal : GenericRepository<Basket>, IBasketDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfBasketDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteBasketByCustomerCode(string code)
        {
            var values = _context.Baskets.Where(x => x.CustomerCode == code).ToList();
            _context.RemoveRange(values);
            _context.SaveChanges();
        }

        public List<Basket> GetBasketByCustomerCodeWithProductName(string code)
        {
            return _context.Baskets.Where(x => x.CustomerCode == code).Include(y => y.Product).ToList();
        }
    }
}
