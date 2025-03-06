using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using PlannedToAT.Models.StudentModels;
using PlannedToAT.Models.AdminModels;
using Microsoft.EntityFrameworkCore;

namespace PlannedToAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Profile()
        {
            // Hardcoded student data
            var model = new SignUpStudent
            {
                StudentName = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "1234567890",
                Institution = "unf",
                SubgroupOrTeam = "A"
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProfile(SignUpStudent model)
        {
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        public IActionResult Forms() {
            ViewData["Layout"] = "_StudentDashboardLayout";
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            // Hardcoded password change simulation
            string currentPasswordInSystem = "OldPassword123";  // This is the hardcoded current password.

            if (CurrentPassword != currentPasswordInSystem)
            {
                TempData["ErrorMessage"] = "Invalid current password!";
                return RedirectToAction("Profile");
            }

            if (NewPassword != ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match!";
                return RedirectToAction("Profile");
            }

            // Assuming the new password change is successful
            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Profile");
        }

        // Displays the main sign-up page
        public IActionResult SignUp()
        {
            return View();
        }

        // Displays the student sign-up page
        public IActionResult SignUpStudent()
        {
            return View();
        }
        public IActionResult StudentDashboard(string studentName, DateTime dob, string race, string phone, string email, string institution, string subgroup)
        {
            var model = new SignUpStudent
            {
                StudentName = studentName,
                DateOfBirth = dob,
                RaceEthnicity = race,
                PhoneNumber = phone,
                EmailAddress = email,
                Institution = institution,
                SubgroupOrTeam = subgroup
            };

            return View(model);
        }

        public IActionResult SignUpAdmin()
        {
            return View("~/Views/AdminViews/AdminSignUp.cshtml");
        }


        // Handles admin form submission
        [HttpPost]
        public IActionResult SignUpAdmin(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("AdminDashboard", new { firstName = model.FirstName });
            }

            return View(model);
        }

        // Displays admin dashboard
        public IActionResult AdminDashboard(string firstName)
        {
            var model = new AdminInputFormModel
            {
                FirstName = firstName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
