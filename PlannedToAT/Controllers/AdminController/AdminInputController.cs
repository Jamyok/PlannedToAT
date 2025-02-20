using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models.AdminModels;
using PlannedToAT.Models;
using System.Collections.Generic;

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

        // Logout (POST method)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        // ================================
        // 🛠 Survey Management Logic
        // ================================

        // Display the survey management page
        //[Authorize(Roles = "Admin")]
        public IActionResult ManageSurvey()
        {
            var model = new SurveyManagementModel
            {
                SurveyTitle = "Student Feedback Survey",
                Questions = new List<SurveyQuestion>
                {
                    new SurveyQuestion { Text = "How would you rate the program?", Type = "Radio", Options = "Excellent,Good,Neutral,Poor" },
                    new SurveyQuestion { Text = "What did you find most valuable?", Type = "Textarea" },
                    new SurveyQuestion { Text = "Would you recommend this program?", Type = "Radio", Options = "Yes,No" }
                }
            };

            return View("~/Views/AdminViews/ManageSurvey.cshtml", model);
        }

        // Save survey changes (POST method)
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSurveyChanges(SurveyManagementModel model)
        {
            if (ModelState.IsValid)
            {
                // Save changes to the database (placeholder for now)
                return RedirectToAction("SurveyUpdateSuccess");
            }

            return View("~/Views/AdminViews/ManageSurvey.cshtml", model);
        }

        // Success confirmation page after updating survey
        public IActionResult SurveyUpdateSuccess()
        {
            return View("~/Views/AdminViews/SurveyUpdateSuccess.cshtml");
        }
    }

    //[Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
