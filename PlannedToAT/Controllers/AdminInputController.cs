using Microsoft.AspNetCore.Mvc;
using AdminUser.Models;

namespace AdminUser.Controllers
{
    public class AdminInputController : Controller
    {
        // GET: Display the form (changed view name to Admin)
        public IActionResult Admin()
        {
            return View();
        }

        // POST: Handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic for processing the form data goes here, e.g., saving to a database

                // Redirect to Success page upon successful submission
                return RedirectToAction("Success");
            }

            // If there are validation errors, re-display the form with validation messages
            return View("Admin", model);
        }

        // GET: Display success page
        public IActionResult Success()
        {
            return View();
        }
    }
}