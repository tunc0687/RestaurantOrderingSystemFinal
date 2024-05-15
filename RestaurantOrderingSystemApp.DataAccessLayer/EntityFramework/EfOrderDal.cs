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
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfOrderDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public int ActiveOrderCount()
        {
            return _context.Orders
                .Where(x => x.Status == true)
                .Count();
        }

        public void CloseAccount(int id)
        {
            var value = _context.Orders
                .Where(x => x.OrderID == id)
                .FirstOrDefault();
            if (value != null)
            {
                value.Status = false;
            }
            _context.SaveChanges();
        }

        public Order? FindOrderByMenuTableId(int id)
        {
            return _context.Orders
                .Where(x => x.MenuTableID == id)
                .Where(y => y.Status == true)
                .Include(z => z.MenuTable)
                .FirstOrDefault();

        }

        public Order? FindOrderWithMenuTableName(int id)
        {
            return _context.Orders
                .Where(x => x.OrderID == id)
                .Include(z => z.MenuTable)
                .FirstOrDefault();
        }

        public List<Order> GetOrdersWithMenuTableNames()
        {
            var values = _context.Orders.Include(x => x.MenuTable).ToList();
            return values;
        }

        public decimal LastOrderPrice()
        {
            return _context.Orders
                .OrderByDescending(x => x.OrderID)
                .Take(1)
                .Select(y => y.FinalPrice)
                .FirstOrDefault();
        }

        public decimal TodayTotalPrice()
        {
            return _context.Orders
                .Where(x => x.Status == true)
                .Where(y => y.OrderDate == DateTime.Now.Date)
                .Sum(z => z.FinalPrice);
        }

        public int TotalOrderCount()
        {
            return _context.Orders.Count();
        }
    }
}
