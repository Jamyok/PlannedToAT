using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using System.Linq;
using System.Collections.Generic;
using PlannedToAT.Models;


namespace StudentManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to handle the AdminReports form submission and display selected report
        [HttpGet]
        public IActionResult AdminReports()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminReports(string reportSelection)
        {
            ViewBag.ReportType = reportSelection;

            // Get data based on the selected report
            switch (reportSelection)
            {
                case "TotalStudents":
                    ViewBag.ReportTitle = "Total Number of Students";
                    ViewBag.TotalStudents = _context.Students.Count();
                    break;

                case "StudentsWithBankAccounts":
                    ViewBag.ReportTitle = "Students with Bank Accounts";
                    ViewBag.StudentsWithBankAccounts = _context.Students.Count(s => s.HasBankAccount == true);
                    break;

                case "StudentDetails":
                    ViewBag.ReportTitle = "Student Details";
                    ViewBag.StudentDetails = _context.Students
                        .Select(s => new 
                        {
                            s.FirstName,
                            s.LastName,
                            s.School,
                            s.Organization,
                            s.GraduatingYear,
                            s.HasBankAccount
                        })
                        .ToList();
                    break;

                default:
                    ViewBag.ReportTitle = "Invalid Report Selection";
                    break;
            }

            return View();
        }
    }
}