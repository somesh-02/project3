using coreCodeFirstApproachProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace coreCodeFirstApproachProject.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "user")]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var brand = _context.Brands.Join(
                _context.Products,
                c => c.BrandId,
                p => p.ProductId,
                (c, p) => new
                {
                    productId = p.ProductId,
                    productName = p.Name,
                    price = p.Price,
                    brandId = c.BrandId,
                    brandName = c.Name,
                }).ToList();
            ViewBag.BrandName = brand;
            var category = _context.Categories.Join(
               _context.Products,
               c => c.CategoryId,
               p => p.ProductId,
               (c, p) => new
               {
                   productId = p.ProductId,
                   productName = p.Name,
                   price = p.Price,
                   categoryId = c.CategoryId,
                   categoryName = c.Name,
               }).ToList();
            ViewBag.CategoryName = category;
            return View(products);
        }
        public IActionResult ViewDetails(int id)
        {
            var data = _context.Products.FirstOrDefault(x => x.ProductId == id);
            return View(data);
        }
    }
}
