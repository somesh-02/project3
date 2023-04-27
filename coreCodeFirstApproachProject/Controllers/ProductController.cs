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
        
        public IActionResult Index()
        {
            var products = _context.Products.Select(p => new
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Image= p.Image,
                Quantity = p.AvailableQuantity,
                BrandName = p.Brand.Name,
                CategoryName = p.Category.Name,
                Date = p.ProductAddedDate,
            }).ToList();
            return View(products);
        }
        public IActionResult ViewDetails(int id)
        {
            /*var data = _context.Products.FirstOrDefault(x => x.ProductId == id);*/
            var products = _context.Products.Where(p => p.ProductId == id).Select(p => new
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Quantity = p.AvailableQuantity,
                BrandName = p.Brand.Name,
                CategoryName = p.Category.Name,
                Date = p.ProductAddedDate,
            }).ToList();
            ViewBag.Result = products;
            return View(products);
        }
    }
}
