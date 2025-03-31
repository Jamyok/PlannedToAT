using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using PlannedToAT.Models.AdminModels;
using PlannedToAT.Services;

namespace PlannedToAT.Controllers.AdminController
{
    public class ReportsController : Controller
    {

        //figure out the admindbcontext change
        //add the csvimport
        private readonly ApplicationDbContext _context;
        /*private readonly CsvImportService _csvImportService;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            _csvImportService = new CsvImportService(_context);
            _csvImportService.ImportCsv("wwwroot/data/Participants-All_data_fields.csv"); 
        }*/

        

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
                    var totalStudentsCount = _context.ReportData.Count();
                    ViewData["ReportTitle"] = "Total Number of Students";
                    ViewData["ReportData"] = totalStudentsCount.ToString();
                    return View("~/Views/AdminViews/Reports.cshtml");

                case "StudentsWithBankAccounts":
                    reportData = _context.ReportData
                        .Where(student => student.HasBankAccount == true)
                        .ToList();
                    ViewData["ReportTitle"] = "Students with Bank Accounts";
                    break;

                case "StudentDetails":
                    reportData = _context.ReportData.ToList();
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
