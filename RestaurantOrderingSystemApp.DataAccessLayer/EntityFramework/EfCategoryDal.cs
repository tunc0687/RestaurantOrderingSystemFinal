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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfCategoryDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public int ActiveCategoryCount()
        {
            return _context.Categories.Where(x => x.Status == true).Count();
        }

        public int CategoryCount()
        {
            return _context.Categories.Count();
        }

        public int PassiveCategoryCount()
        {
            return _context.Categories.Where(x => x.Status == false).Count();
        }
    }
}
