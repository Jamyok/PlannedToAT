using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using Newtonsoft.Json;
using PlannedToAT.Models;
using PlannedToAT.Models.StudentModels;
using PlannedToAT.Models.AdminModels;

namespace PlannedToAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
        return LocalRedirect("~/Identity/Account/Login");
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
