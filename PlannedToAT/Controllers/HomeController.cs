using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using Newtonsoft.Json;
using PlannedToAT.Models;
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

        public IActionResult LogIn()
        {
        return LocalRedirect("~/Identity/Account/Login");
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
            return LocalRedirect("~/Identity/Account/Register");
        }

        // Displays the student sign-up page
        [HttpGet]
        public IActionResult SignUpStudent()
        {
            return LocalRedirect("~/Identity/Account/Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitStudentForm(SignUpStudent studentData)
        {
            if (ModelState.IsValid)
            {
                TempData["StudentData"] = JsonConvert.SerializeObject(studentData);
                return RedirectToAction("StudentDashboard");
            }
            return View("SignUpStudent", studentData);
        }

        public IActionResult StudentDashboard()
        {
            if (TempData["StudentData"] is string studentJson)
            {
                var model = JsonConvert.DeserializeObject<SignUpStudent>(studentJson);
                return View(model);
            }
            return RedirectToAction("SignUpStudent");
        }

        [HttpGet]
        public IActionResult SignUpAdmin()
        {
            return View("~/Views/AdminViews/AdminSignUp.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdminForm(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["AdminFirstName"] = model.FirstName;
                return RedirectToAction("AdminDashboard");
            }
            return View("SignUpAdmin", model);
        }

        public IActionResult AdminDashboard()
        {
            var model = new AdminInputFormModel
            {
                FirstName = TempData["AdminFirstName"] as string
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
