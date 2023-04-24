using coreCodeFirstApproachProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
    }
}
