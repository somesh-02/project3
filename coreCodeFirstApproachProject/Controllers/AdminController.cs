using coreCodeFirstApproachProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;

namespace coreCodeFirstApproachProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles ="admin")]
        public IActionResult Index()
        {
            var data = _context.Users.ToList();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User1 user)
        {
            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == id);
            return View(data);
        }
        [Authorize(Roles = "admin")]
        public IActionResult DisplayBrands()
        {
            var data = _context.Brands.ToList();
            return View(data);
        }
        [Authorize(Roles = "admin")]
        public IActionResult DisplayCategories()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }
        public ActionResult BrandDetails(int id)
        {
            var data = _context.Brands.FirstOrDefault(x => x.BrandId == id);
            return View(data);
        }
        public ActionResult BrandDelete(int id)
        {
            var data = _context.Brands.FirstOrDefault(x => x.BrandId == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandDelete(int id, Brand brand)
        {
            try
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
                return RedirectToAction("DisplayBrands");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                ViewBag.Message = HtmlEncoder.Default.Encode("Invalid Id of the Author");
                return View();
            }
            else
            {
                var data = _context.Brands.FirstOrDefault(x => x.BrandId == id);
                return View(data);
            }
        }
        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return RedirectToAction("DisplayBrands");
        }
        public ActionResult CategoryDetails(int id)
        {
            var data = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            return View(data);
        }
        public IActionResult EditCategory(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                ViewBag.Message = HtmlEncoder.Default.Encode("Invalid Id of the Author");
                return View();
            }
            else
            {
                var data = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
                return View(data);
            }
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
        public ActionResult DeleteCategory(int id)
        {
            var data = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id, Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("DisplayCategories");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult CreateBrand()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBrand(Brand brand)
        {
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return RedirectToAction("DisplayBrands");
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
        public IActionResult CreateProduct()
        {
             var brands = _context.Products.Select(a => new SelectListItem { Value = a.BrandId.ToString(), Text = a.Brand.Name }).Distinct().ToList();
            var categories = _context.Products.Select(a => new SelectListItem { Value = a.CategoryId.ToString(), Text = a.Category.Name }).Distinct().ToList();
            ViewBag.brands = brands;
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product pro)
        {
            _context.Products.Add(pro);
            _context.SaveChanges();
            return View("ViewProducts");
        }
        [Authorize(Roles = "admin")]
        public IActionResult ViewProducts()
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
    }
}
