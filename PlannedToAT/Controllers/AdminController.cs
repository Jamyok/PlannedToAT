using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;

namespace AdminUser.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminReports()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                var applicationDbContext = new ApplicationDbContext();
                applicationDbContext.Add(model);
                applicationDbContext.SaveChanges();
                // Logic to create the admin user (save to database, etc.)
                return RedirectToAction("AdminDashboard", "Home", new { firstName = model.FirstName });
            }

            return View("Admin", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Implement your logout logic here
            return RedirectToAction("Index", "Home");
        }
    }
}
