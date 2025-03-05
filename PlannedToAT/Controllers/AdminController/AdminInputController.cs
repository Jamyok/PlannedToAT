using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models.AdminModels;
using System.Threading.Tasks; // Required for async

namespace AdminUser.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminInputController : Controller
    {
        public IActionResult Admin()
        {
            return View("~/Views/AdminViews/AdminSignUp.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "AdminDashboard", new { firstName = model.FirstName });
            }

            return View("Admin", model);
        }

        public IActionResult AdminDashboard(string firstName)
        {
            var model = new AdminInputFormModel { FirstName = firstName };
            return View("~/Views/AdminViews/AdminDashboard.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index(string firstName)
        {
            var model = new AdminInputFormModel { FirstName = firstName };
            return View(model);
        }
    }
}
