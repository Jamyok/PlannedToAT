using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
//I need to commit
namespace PlannedToAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Add items as webpagges\\
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignUpStudent()
        {
            return View();
        }

        //change this later
        public IActionResult AdminReports()
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
        public IActionResult AdminDashboard(string firstName)
        {
            var model = new AdminInputFormModel
            {
                FirstName = firstName
                // Initialize other properties if necessary
            };

            return View("AdminDashboard", model); // Ensure the view name matches your file name
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ViewEula()
        {
            return View();
        }
    }
}
