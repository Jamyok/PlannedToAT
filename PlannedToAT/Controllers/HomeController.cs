using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
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

        public IActionResult Privacy()
        {
            return View();
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
            var model = new StudentSignUpModel
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
    }
}
