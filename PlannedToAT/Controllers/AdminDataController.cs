/*using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PlannedToAT.Controllers
{
    public class AdminDataController : Controller
    {
        private readonly AdminStudentDataModel _context;

        public AdminDataController(AdminStudentDataModel context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminReports()
        {
            // Display the Admin Reports form
            return View();
        }

        [HttpPost]
        /*public async Task<IActionResult> AdminReports(string reportSelection)
        {
            if (string.IsNullOrEmpty(reportSelection))
            {
                ModelState.AddModelError("", "Please select a report.");
                return View("/Views/Admin/AdminReports.cshtml");
            }

            ViewData["ReportType"] = reportSelection;

            if (reportSelection == "TotalStudents")
            {
                ViewData["TotalCount"] = await _context.StudentPersonalData.CountAsync();
            }
            else if (reportSelection == "StudentsWithBankAccounts")
            {
                ViewData["BankAccountCount"] = await _context.StudentPersonalData.CountAsync(s => s.HasBankAccount);
            }
            else if (reportSelection == "StudentDetails")
            {
                var studentDetails = await _context.StudentPersonalData
                    .Select(s => s)
                    .ToListAsync();
                return View("/Views/Admin/AdminReports.cshtml", studentDetails);
            }

            return View("/Views/Admin/AdminReports.cshtml");
        }

        public IActionResult AdminReports(string reportSelection)
        {
            if (string.IsNullOrEmpty(reportSelection))
            {
                ViewData["ReportTitle"] = "Invalid Report Selection";
                return View(Enumerable.Empty<StudentPersonalData>());
            }

            IEnumerable<StudentPersonalData> reportData = Enumerable.Empty<StudentPersonalData>();

            switch (reportSelection)
            {
                case "TotalStudents":
                    var totalStudentsCount = _context.StudentPersonalData.Count();
                    ViewData["ReportTitle"] = "Total Number of Students";
                    ViewData["ReportData"] = totalStudentsCount.ToString();
                    return View(reportData);

                case "StudentsWithBankAccounts":
                    reportData = (from student in _context.StudentPersonalData
                                  where student.HasBankAccount == true
                                  select student).ToList();
                    ViewData["ReportTitle"] = "Students with Bank Accounts";
                    break;

                case "StudentDetails":
                    reportData = (from student in _context.StudentPersonalData
                                  select student).ToList();
                    ViewData["ReportTitle"] = "All Student Details";
                    break;

                default:
                    ViewData["ReportTitle"] = "Invalid Report Selection";
                    break;
            }

             // Add debugging
            Console.WriteLine("Report Data: " + reportData.Count());


            return View(reportData);
        }
    }
}*/
