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
    public class EfFeatureDal : GenericRepository<Feature>, IfeatureDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfFeatureDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeStatus(int id)
        {
            var result = _context.Features.Find(id);
            if (result != null)
            {
                result.Status = !result.Status;
                _context.SaveChanges();
            }
        }

        public List<Feature> GetFeaturesByStatusTrue()
        {
            return _context.Features
                .Where(x => x.Status == true)
                .ToList();
        }
    }
}
