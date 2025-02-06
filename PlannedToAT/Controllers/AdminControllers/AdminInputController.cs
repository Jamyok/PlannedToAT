using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using PlannedToAT.Models.AdminModels;
using System.Linq;


namespace AdminUser.Controllers
{
    public class AdminInputController : Controller
    {
        public IActionResult Admin()
        {
            return View("~/Views/Admin/Admin.cshtml");
        }
        public IActionResult AdminReports()
        {
            return View("/Views/AdminViews/Reports.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AdminInputFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to create the admin user (save to database, etc.)
                return RedirectToAction("AdminDashboard", "Admin", new { firstName = model.FirstName });
            }

            return View("Admin", model);
        }

        public IActionResult AdminDashboard(string firstName)
        {
            var model = new AdminInputFormModel
            {
                FirstName = firstName
            };
            return View("~/Views/Admin/AdminDashboard.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Implement your logout logic here
            return RedirectToAction("Index", "Home");
        }
    }
}


/*namespace PlannedToAT.Controllers.Admin
{
    public class AdminInputController : Controller
    {
        private readonly AdminDbContext _context;

        public AdminInputController(AdminDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminReports()
        {
            // Display the Admin Reports form
            return View("~/Views/Admin/AdminReports.cshtml");
        }

        [HttpPost]
        public IActionResult AdminReports(string reportSelection)
        {
            if (string.IsNullOrEmpty(reportSelection))
            {
                ViewData["ReportTitle"] = "Invalid Report Selection";
                return View("~/Views/Admin/AdminReports.cshtml",Enumerable.Empty<StudentPersonalData>());
            }

            IEnumerable<StudentPersonalData> reportData = Enumerable.Empty<StudentPersonalData>();

            switch (reportSelection)
            {
                case "TotalStudents":
                    var totalStudentsCount = _context.StudentPersonalData.Count();
                    ViewData["ReportTitle"] = "Total Number of Students";
                    ViewData["ReportData"] = totalStudentsCount.ToString();
                    return View("~/Views/Admin/AdminReports.cshtml", reportData);

                case "StudentsWithBankAccounts":
                    reportData = _context.StudentPersonalData
                        .Where(student => student.HasBankAccount == true)
                        .ToList();
                    ViewData["ReportTitle"] = "Students with Bank Accounts";
                    break;

                case "StudentDetails":
                    reportData = _context.StudentPersonalData.ToList();
                    ViewData["ReportTitle"] = "All Student Details";
                    break;

                default:
                    ViewData["ReportTitle"] = "Invalid Report Selection";
                    break;
            }

            return View("Views/AdminViews/Reports.cshtml", reportData);
        }
    }
}
*/