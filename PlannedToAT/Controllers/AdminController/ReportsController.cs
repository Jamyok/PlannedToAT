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
                    int total = dbContext.CsvImportData.Count();
                    ViewData["ReportTitle"] = "Total Number of Students";
                    ViewData["ReportData"] = total;
                    return View("~/Views/AdminViews/Reports.cshtml");

                case "StudentsWithBankAccounts":
                    reportData = dbContext.CsvImportData
                        .Where(s => s.HasBankAccount == true)
                        .ToList();
                    ViewData["ReportTitle"] = "Students with Bank Accounts";
                    ViewData["Headers"] = new List<string> { "First Name", "Last Name", "State", "Has Bank Account" };
                    ViewData["Fields"] = new List<string> { "FirstName", "LastName", "State", "HasBankAccount" };
                    break;

                case "StudentDetails":
                    reportData = dbContext.CsvImportData.ToList();
                    ViewData["ReportTitle"] = "Student Details";
                    ViewData["Headers"] = new List<string> { "First Name", "Last Name", "Phone", "Email", "DOB", "Accounts", "Cohorts", "State" };
                    ViewData["Fields"] = new List<string> { "FirstName", "LastName", "PhoneNumber", "Email", "DOB", "Accounts", "Cohorts", "State" };
                    break;

                case "StudentsByUniversity":
                    reportData = dbContext.CsvImportData
                        .OrderBy(student => student.Cohorts)
                        .ToList();

                    ViewData["ReportTitle"] = "Students by University";
                    ViewData["Headers"] = new List<string> { "First Name", "Last Name", "State" }; // <- Add "State"
                    ViewData["Fields"] = new List<string> { "FirstName", "LastName", "Cohorts" };   // <- Remember, Cohorts = University/State
                    break;
                    
                case "StudentAnswers":
                    reportData = dbContext.CsvImportData.ToList();
                    ViewData["ReportTitle"] = "Student Answers";
                    ViewData["Headers"] = new List<string> { "First Name", "Last Name", "Needs/Wants", "Saving Goal", "SMART Goal", "Family/Friends" };
                    ViewData["Fields"] = new List<string> { "FirstName", "LastName", "NeedsWants", "SavingGoal", "SMARTGoal", "FamilyFriends" };
                    break;

                case "SavingsProgress":
                    reportData = dbContext.CsvImportData.ToList();
                    ViewData["ReportTitle"] = "Savings Progress (Start)";
                    ViewData["Headers"] = new List<string> { "First Name", "Last Name", "Checking $ Start", "Savings $ Start", "Investing $ Start" };
                    ViewData["Fields"] = new List<string> { "FirstName", "LastName", "CheckingBalanceStart", "SavingsBalanceStart", "InvestingBalanceStart" };
                    break;

                default:
                    ViewData["ReportTitle"] = "Invalid Report Selection";
                    break;
            }

            return View("~/Views/AdminViews/Reports.cshtml", reportData);
        }
    }
}
