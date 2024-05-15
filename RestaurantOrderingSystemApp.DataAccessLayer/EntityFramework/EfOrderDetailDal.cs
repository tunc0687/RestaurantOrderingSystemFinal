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
    public class EfOrderDetailDal : GenericRepository<OrderDetail>, IOrderDetailDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfOrderDetailDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeDescription(int id)
        {
            var value = _context.OrderDetails
                .Where(x => x.OrderDetailID == id)
                .FirstOrDefault();
            if (value != null)
            {
                if (value.Description == "Sipariş Alındı")
                {
                    value.Description = "Sipariş Hazırlanıyor";
                }
                else if (value.Description == "Sipariş Hazırlanıyor")
                {
                    value.Description = "Sipariş Gönderildi";
                }
            }
            _context.SaveChanges();
        }

        public List<OrderDetail> GetOrderDetailsByCustomerCodeWithProducts(string code)
        {
            var values = _context.OrderDetails
                .Where(x => x.CustomerCode == code)
                .Include(y => y.Product)
                .ToList();
            return values;
        }

        public List<OrderDetail> GetOrderDetailsWithProducts(int id)
        {
            var values = _context.OrderDetails
                .Where(x => x.OrderID == id)
                .Include(y => y.Product)
                .ToList();
            return values;
        }
    }
}
