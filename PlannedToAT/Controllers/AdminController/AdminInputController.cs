using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models.AdminModels;
using PlannedToAT.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PlannedToAT.ViewModels;

namespace AdminUser.Controllers
{
    public class AdminInputController(ApplicationDbContext dbContext) : Controller

    {
        private static SurveyManagementModel _surveyModel = new SurveyManagementModel
        {
            SurveyTitle = "Student Feedback Survey",
            Questions = new List<SurveyQuestion>
            {
                new SurveyQuestion { Text = "How would you rate the program?", Type = "Radio", Options = "Excellent,Good,Neutral,Poor" },
                new SurveyQuestion { Text = "What did you find most valuable?", Type = "Textarea" },
                new SurveyQuestion { Text = "Would you recommend this program?", Type = "Radio", Options = "Yes,No" }
            }
        };

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
                dbContext.AdminSignUp.Add(model);
                dbContext.SaveChanges();
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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Redirects to landing page
        }


        // ================================
        // 🛠 Survey Management Logic
        // ================================

        // Display the survey management page
        public IActionResult ManageSurvey()
        {
            return View("~/Views/AdminViews/ManageSurvey.cshtml", _surveyModel);
        }
        
        public IActionResult Settings()
        {
            var admin = dbContext.AdminSignUp.FirstOrDefault();
            if (admin == null) return RedirectToAction("AdminDashboard", "Home");

            var model = new AdminSettingsViewModel
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            };

            return View("~/Views/AdminViews/Settings.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Settings")]
        public IActionResult UpdateSettings(AdminSettingsViewModel updated)
        {
            var adminEmail = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(adminEmail))
            {
                TempData["Error"] = "Could not identify the admin user.";
                return RedirectToAction("Settings");
            }

            var admin = dbContext.AdminSignUp.FirstOrDefault(a => a.Email == adminEmail);

            if (!string.IsNullOrWhiteSpace(updated.FirstName)) admin.FirstName = updated.FirstName;
            if (!string.IsNullOrWhiteSpace(updated.LastName)) admin.LastName = updated.LastName;
            if (!string.IsNullOrWhiteSpace(updated.Email)) admin.Email = updated.Email;

            if (!string.IsNullOrWhiteSpace(updated.Password) && updated.Password == updated.ConfirmPassword)
            {
                admin.Password = updated.Password;
            }

            dbContext.SaveChanges();
            TempData["Success"] = "Account settings updated successfully.";

            return RedirectToAction("Settings");
        }


        // Save survey changes and update student survey (POST method)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSurveyChanges(SurveyManagementModel model)
        {
            if (ModelState.IsValid)
            {
                _surveyModel = model; // Update the admin-side survey model

                // Redirect to update the student survey
                return RedirectToAction("UpdateSurvey", "StudentSurvey", new { SurveyTitle = model.SurveyTitle });
            }

            return View("~/Views/AdminViews/ManageSurvey.cshtml", model);
        }

        // Display the survey form for creating a new survey
        public IActionResult SurveyForm()
        {
            var model = new SurveyManagementModel
            {
                SurveyTitle = "Admin Survey",
                Questions = new List<SurveyQuestion>
                {
                    new SurveyQuestion { Text = "What improvements would you like to see?", Type = "Textarea" },
                    new SurveyQuestion { Text = "How would you rate the system usability?", Type = "Radio", Options = "Excellent,Good,Neutral,Poor" },
                    new SurveyQuestion { Text = "Would you recommend this system?", Type = "Radio", Options = "Yes,No" }
                }
            };

            return View("~/Views/AdminViews/ManageSurvey.cshtml", model);
        }

        // Success confirmation page after updating survey
        public IActionResult SurveyUpdateSuccess()
        {
            return View("~/Views/AdminViews/SurveyUpdateSuccess.cshtml");
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