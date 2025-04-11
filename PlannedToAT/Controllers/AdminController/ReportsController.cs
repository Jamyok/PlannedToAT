using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using PlannedToAT.Models.AdminModels;



namespace PlannedToAT.Controllers.AdminController
{
    public class ReportsController(ApplicationDbContext dbContext) : Controller
    {


        [HttpGet]
        public IActionResult Reports()
        {
            return View("~/Views/AdminViews/Reports.cshtml");
        }

        [HttpPost]
        public IActionResult Reports(string reportSelection)
        {
            if (string.IsNullOrEmpty(reportSelection))
            {
                ViewData["ReportTitle"] = "Invalid Report Selection";
                return View("~/Views/AdminViews/Reports.cshtml", Enumerable.Empty<ReportsModel>());
            }

            IEnumerable<ReportsModel> reportData = Enumerable.Empty<ReportsModel>();

            ViewData["ReportDate"] = DateTime.Now.ToString("MMMM dd, yyyy");

            switch (reportSelection)
            {
                case "TotalStudents":
                    var totalStudentsCount = dbContext.CsvImportData.Count();
                    ViewData["ReportTitle"] = "Total Number of Students";
                    ViewData["ReportData"] = totalStudentsCount.ToString();
                    return View("~/Views/AdminViews/Reports.cshtml");

                case "StudentsWithBankAccounts":
                    reportData = dbContext.CsvImportData
                        .Where(student => student.HasBankAccount == true)
                        .ToList();
                    ViewData["ReportTitle"] = "Students with Bank Accounts";
                    break;

                case "StudentDetails":
                    reportData = dbContext.CsvImportData.ToList();
                    ViewData["ReportTitle"] = "All Student Details";
                    break;

                default:
                    ViewData["ReportTitle"] = "Invalid Report Selection";
                    break;
            }

            return View("~/Views/AdminViews/Reports.cshtml", reportData);
        }
    }
}
