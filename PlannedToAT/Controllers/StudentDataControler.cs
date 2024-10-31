using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using System.Linq;

namespace StudentManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        } 
        

        // Action to show the total number of students
        public IActionResult TotalStudents()
        {
            int totalStudents = _context.Students.Count();
            ViewBag.TotalStudents = totalStudents;
            return View();
        }

        // Action to show the number of students with bank accounts
        public IActionResult StudentsWithBankAccounts()
        {
            int studentsWithBankAccounts = _context.Students.Count(s => s.HasBankAccount == true);
            ViewBag.StudentsWithBankAccounts = studentsWithBankAccounts;
            return View();
        }

        // Action to show a list of students with their details
        public IActionResult StudentDetails()
        {
            var students = _context.Students
                .Select(s => new 
                {
                    s.Name,
                    s.School,
                    s.Organization,
                    s.GraduatingYear
                }).ToList();
                
            return View(students);
        }
    }
}