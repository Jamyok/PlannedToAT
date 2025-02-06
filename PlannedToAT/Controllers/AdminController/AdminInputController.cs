using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models.AdminModels;

namespace AdminUser.Controllers
{
    public class AdminInputController : Controller
    {
        // Display the Admin sign-up form (GET method)
        public IActionResult Admin()
        {
            return View("~/Views/AdminViews/AdminSignUp.cshtml");
        }

        // Handle the form submission (POST method)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AdminDashboard", "Home", new { firstName = model.FirstName });
            }

            return View("Admin", model);
        }

        // Admin dashboard (GET method)
        public IActionResult AdminDashboard(string firstName)
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
            return RedirectToAction("Index", "Home");
        }
    }
}
