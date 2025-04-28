using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using PlannedToAT.Models.StudentModels;
using PlannedToAT.Models.AdminModels;
using LoadCsv;
using LoadCsv.Models;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.ViewModels;
using LoadCsv.Services;

namespace PlannedToAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private readonly ImportCsvDbContext _csvContext;
        private readonly AdminCsvImportService _adminCsvImportService;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, ImportCsvDbContext csvContext, AdminCsvImportService adminCsvImportService)
        {
            _logger = logger;
            _context = context;
            _csvContext = csvContext;
            _adminCsvImportService = adminCsvImportService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/Student/Profile")]
       public IActionResult Profile()
{
    var studentEmail = User.Identity?.Name;
    var student = _csvContext.CsvImportData.FirstOrDefault(s => s.Email == studentEmail);

    if (student == null)
    {
        TempData["ErrorMessage"] = "Student not found.";
        return RedirectToAction("StudentDashboard");
    }

    var model = new SignUpStudent
    {
        StudentName = student.FullName,
        EmailAddress = student.Email,
        PhoneNumber = student.PhoneNumber,
        Institution = student.Cohorts,
        SubgroupOrTeam = "A" 
    };

    return View("~/Views/StudentViews/Profile.cshtml", model);
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
       public IActionResult StudentDashboard()
        {
            var studentEmail = User.Identity?.Name;
            var reports = _csvContext.CsvImportData
                .Where(r => r.Email == studentEmail)
                .ToList();

            // Bar chart: savings over time (group by month of SavingsStart)
            var savingsByMonth = reports
                .Where(r => r.SavingsStart.HasValue && r.SavingsBalanceStart.HasValue)
                .GroupBy(r => r.SavingsStart.Value.ToString("MMM"))
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(r => r.SavingsBalanceStart ?? 0)
                }).ToList();

            // Column chart: upcoming session signups
            var sessionDates = reports
                .SelectMany(r => new[] { r.Session2Signup, r.Session3Signup })
                .Where(d => d.HasValue)
                .GroupBy(d => d.Value.ToString("MMM dd"))
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                }).ToList();

            // Pie chart: count how many filled out each key form
            var completedCount = reports.Count(r =>
                !string.IsNullOrEmpty(r.SMARTGoal) &&
                !string.IsNullOrEmpty(r.NeedsWants) &&
                !string.IsNullOrEmpty(r.ExitTickets));

            var inProgressCount = reports.Count(r =>
                (!string.IsNullOrEmpty(r.SMARTGoal) || !string.IsNullOrEmpty(r.NeedsWants)) &&
                string.IsNullOrEmpty(r.ExitTickets));

            var notStartedCount = reports.Count(r =>
                string.IsNullOrEmpty(r.SMARTGoal) &&
                string.IsNullOrEmpty(r.NeedsWants) &&
                string.IsNullOrEmpty(r.ExitTickets));

            var model = new StudentDashboardViewModel
            {
                StudentName = reports.FirstOrDefault()?.FullName ?? "Student",

                SavingsMonths = savingsByMonth.Select(x => x.Month).ToList(),
                MonthlySavings = savingsByMonth.Select(x => (int)x.Total).ToList(),

                FormDueDates = sessionDates.Select(x => x.Date).ToList(),
                FormsDue = sessionDates.Select(x => x.Count).ToList(),

                CompletedCount = completedCount,
                InProgressCount = inProgressCount,
                NotStartedCount = notStartedCount
            };

            return View("~/Views/StudentViews/StudentDashboard.cshtml", model);

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

        public IActionResult AdminDashboard(string firstName)
        {
            var data = _csvContext.CsvImportData.ToList();
            ViewData["FirstName"] = firstName ?? "Admin";

            if (!data.Any())
            {
                Console.WriteLine("NO DATA FOUND IN CsvImportData");
            }

            // users by state
            var usersByState = data
                .GroupBy(x => x.State)
                .OrderBy(g => g.Key)
                .Select(g => new { State = g.Key ?? "Unknown", Count = g.Count() })
                .ToList();

            // signups by month
            var signupsByMonthRaw = data
                .Where(x => x.Created.HasValue)
                .GroupBy(x => new { x.Created.Value.Year, x.Created.Value.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .ToList();

            var months = signupsByMonthRaw
                .Select(g => new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy"))
                .ToList();

            var signups = signupsByMonthRaw
                .Select(g => g.Count())
                .ToList();

            // Session signup dates
            var sessionDates = data
                .SelectMany(x => new[] { x.Session2Signup, x.Session3Signup })
                .Where(x => x.HasValue)
                .GroupBy(x => x.Value.ToString("MMM dd"))
                .OrderBy(g => g.Key)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToList();

            // Average balances
            var avgChecking = data
                .Where(x => x.CheckingBalanceStart.HasValue)
                .DefaultIfEmpty()
                .Average(x => x?.CheckingBalanceStart ?? 0);

            var avgSavings = data
                .Where(x => x.SavingsBalanceStart.HasValue)
                .DefaultIfEmpty()
                .Average(x => x?.SavingsBalanceStart ?? 0);

            var avgInvesting = data
                .Where(x => x.InvestingBalanceStart.HasValue)
                .DefaultIfEmpty()
                .Average(x => x?.InvestingBalanceStart ?? 0);

            // Form status counts
            var completedForms = data.Count(x =>
                !string.IsNullOrWhiteSpace(x.SMARTGoal) &&
                !string.IsNullOrWhiteSpace(x.NeedsWants) &&
                !string.IsNullOrWhiteSpace(x.ExitTickets));

            var inProgressForms = data.Count(x =>
                (!string.IsNullOrWhiteSpace(x.SMARTGoal) || !string.IsNullOrWhiteSpace(x.NeedsWants)) &&
                string.IsNullOrWhiteSpace(x.ExitTickets));

            var notStartedForms = data.Count(x =>
                string.IsNullOrWhiteSpace(x.SMARTGoal) &&
                string.IsNullOrWhiteSpace(x.NeedsWants) &&
                string.IsNullOrWhiteSpace(x.ExitTickets));

            // Pass everything into the view model
            var model = new AdminDashboardViewModel
            {
                FirstName = firstName,

                States = usersByState.Select(x => x.State).ToList(),
                UsersPerState = usersByState.Select(x => x.Count).ToList(),

                Months = months,
                SignupsPerMonth = signups,

                AvgChecking = avgChecking,
                AvgSavings = avgSavings,
                AvgInvesting = avgInvesting,

                SessionSignupDates = sessionDates.Select(x => x.Date).ToList(),
                SignupCounts = sessionDates.Select(x => x.Count).ToList(),

                CompletedForms = completedForms,
                InProgressForms = inProgressForms,
                NotStartedForms = notStartedForms
            };
            var balancesDebug = data
    .Where(x => x.CheckingBalanceStart.HasValue)
    .Select(x => x.CheckingBalanceStart)
    .ToList();
Console.WriteLine("Raw Checking Balances:");
foreach (var bal in balancesDebug)
{
    Console.WriteLine(bal);
}

            Console.WriteLine("Months: " + string.Join(", ", model.Months));
Console.WriteLine("SignupsPerMonth: " + string.Join(", ", model.SignupsPerMonth));
Console.WriteLine($"AvgChecking: {model.AvgChecking}, AvgSavings: {model.AvgSavings}, AvgInvesting: {model.AvgInvesting}");

            return View("~/Views/AdminViews/AdminDashboard.cshtml", model);
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
        public IActionResult ReimportCsv()
        {
            try
            {
                var importer = new CsvImportService(_csvContext);
                importer.ImportCsv("/Users/namithayadlapalli/PlannedToAT-15/LoadCSV/data/Participants-All_data_fields.csv");
                return Content("CSV Re-imported successfully!");
            }
            catch (Exception ex)
            {
                return Content("Error during import: " + ex.Message);
            }
        }


    }
}
