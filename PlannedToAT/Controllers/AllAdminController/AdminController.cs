using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;

namespace AdminUser.Controllers
{
    public class AdminInputController : Controller
    {
        
        public IActionResult Admin()
        {
            return View("~/Views/Admin/Admin.cshtml");        
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
                var adminDbContext = new AdminDbContext();
                adminDbContext.Add(model);
                adminDbContext.SaveChanges();
                // Logic to create the admin user (save to database, etc.)
                return RedirectToAction("AdminDashboard", "Home", new { firstName = model.FirstName });
            }

            return View("~/Views/Admin/Admin.cshtml", model);
        }

        public IActionResult Dashboard(string firstName)
        {
            var model = new AdminInputFormModel
            {
                FirstName = firstName
            };
            return View("~/Views/Home/AdminDashboard.cshtml", model);
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
