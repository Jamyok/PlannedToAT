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
                // Logic to create the admin user (save to database, etc.)
                return RedirectToAction("AdminDashboard", "Home", new { firstName = model.FirstName });
            }

            return View("Admin", model);
        }

        public IActionResult Dashboard(string firstName)
        {
            var model = new AdminInputFormModel
            {
                FirstName = firstName
            };
            return View(model);
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
