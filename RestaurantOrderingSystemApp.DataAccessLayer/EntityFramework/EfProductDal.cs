using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystemApp.DataAccessLayer.Abstract;
using RestaurantOrderingSystemApp.DataAccessLayer.Concrete;
using RestaurantOrderingSystemApp.DataAccessLayer.Repositories;
using RestaurantOrderingSystemApp.EntityLayer.Entities;

namespace RestaurantOrderingSystemApp.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        private readonly RestaturantOrderingSystemContext _context;
        public EfProductDal(RestaturantOrderingSystemContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetProductsWithCategories()
        {
            var values = _context.Products.Include(x=>x.Category).ToList();
            return values;

        }

        public int ProductCount()
        {
            return _context.Products.Count();
        }

        public int ProductCountByDrink()
        {
            return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "İçecekler").Select(z => z.CategoryID).FirstOrDefault())).Count();
        }

        public int ProductCountByDessert()
        {
            return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "Tatlılar").Select(z => z.CategoryID).FirstOrDefault())).Count();
        }

        public decimal ProductPriceAvg()
        {
            return _context.Products.Average(x => x.Price);
        }

        public string ProductNameByMaxPrice()
        {
            return _context.Products.Where(x => x.Price == (_context.Products.Max(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public string ProductNameByMinPrice()
        {
            return _context.Products.Where(x => x.Price == (_context.Products.Min(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public decimal ProductAvgPriceByDessert()
        {
            return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "Tatlılar").Select(z => z.CategoryID).FirstOrDefault())).Average(w => w.Price);
        }

        public decimal ProductPriceByAdanaKebap()
        {
            return _context.Products.Where(x => x.ProductName=="Adana Kebap").Select(y => y.Price).FirstOrDefault();
        }

        public decimal TotalPriceByDrinkCategory()
        {
            int id=_context.Categories.Where(x=>x.CategoryName== "İçecekler").Select(y=>y.CategoryID).FirstOrDefault();
            return _context.Products.Where(x => x.CategoryID == id).Sum(y => y.Price);   
        }

        public decimal TotalPriceBySaladCategory()
        {
            int id = _context.Categories.Where(x => x.CategoryName == "Salatalar").Select(y => y.CategoryID).FirstOrDefault();
            return _context.Products.Where(x => x.CategoryID == id).Sum(y => y.Price);
        }

        public decimal TotalProductsPrice()
        {
            return _context.Products.Sum(y => y.Price);
        }
    }
}
