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
    public class EfMenuTableDal : GenericRepository<MenuTable>, IMenuTableDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfMenuTableDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeStatusClose(int id)
        {
            var result = _context.MenuTables.Find(id);
            if (result != null)
            {
                result.Status = false;
                _context.SaveChanges();
            }
        }

        public void ChangeStatusOpen(int id)
        {
            var result = _context.MenuTables.Find(id);
            if (result != null)
            {
                result.Status = true;
                _context.SaveChanges();
            }
        }

        public int MenuTableCount()
        {
            return _context.MenuTables
                .Where(x => x.Status == true)
                .Count();
        }

        public int TotalMenuTableCount()
        {
            return _context.MenuTables
                .Count();
        }
    }
}
