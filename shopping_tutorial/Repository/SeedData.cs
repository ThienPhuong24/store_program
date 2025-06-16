using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;

namespace shopping_tutorial.Repository
{
    public class SeedData
    {
        public static void seedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if(!_context.Products.Any())
            {
                CategoryModel ao = new CategoryModel { Name = "Ao", Slug = "ao", Description = "Cac kieu ao  don gian", Status = 1 };
                CategoryModel quan = new CategoryModel { Name = "Quan", Slug = "quan", Description = "Cac kieu quan dai hot trend", Status = 1 };
                BrandModel routine = new BrandModel { Name = "routine", Slug = "routine", Description = "make it simple but significant", Status = 1 };
                BrandModel mrsimple = new BrandModel { Name = "mrsimple", Slug = "mrsimple", Description = "thoi trang  hot trend", Status = 1 };
                _context.Products.AddRange(
                    new ProductModel { Name = "ao thun trang tron", Slug = "ao thun", Description = "ao thun trang don gian", Image = "1.jpg", Category = ao, Price = 12, Brand = routine },
                    new ProductModel { Name = "quan jean xanh", Slug = "quan jean", Description = "quan jean xanh don gian", Image = "1.jpg", Category = quan, Price = 12, Brand = mrsimple }
                );
                _context.SaveChanges();
            }
        }
    }
}
