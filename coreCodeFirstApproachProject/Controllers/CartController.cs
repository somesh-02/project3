using coreCodeFirstApproachProject.Helpers;
using coreCodeFirstApproachProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace coreCodeFirstApproachProject.Controllers
{
    public class CartController : Controller
    {

        private ApplicationDbContext _context;
        public CartController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "user")]
        public IActionResult Index()
        {
            var cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if(cart.IsNullOrEmpty())
            {
                return RedirectToAction("EmptyCart");
            }
            else
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
                return View();
            }
        }
        public IActionResult Buy(int id)
        {
            if (SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.ProductId == id), Quantity = 1 });
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.ProductId == id), Quantity = 1 });
                }
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public int isExists(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
        [Authorize(Roles = "user")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExists(id);
            cart.RemoveAt(index);
            SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "user")]
        public IActionResult CheckOut()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        public IActionResult MakePayment()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="user")]
        public IActionResult MakePayment(Address addr) 
        {
            _context.Addresses.Add(addr);
            _context.SaveChanges();
            ViewBag.address = addr;
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View(); 
        }

        [Authorize(Roles = "user")]
        public IActionResult EmptyCart()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if(cart.IsNullOrEmpty())
            {
                return View();
            }
            else
            {
                cart.Clear();
            }
            return View();
        }
        
        public IActionResult Payment()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            var addr = _context.Addresses.FirstOrDefault();
            ViewBag.address = addr;
            return View();
        }
    }
}
