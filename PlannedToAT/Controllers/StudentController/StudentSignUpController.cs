using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Required for TempData serialization
using PlannedToAT.Models.StudentModels;
using PlannedToAT.ViewModels;
using LoadCsv;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ImportCsvDbContext _csvContext;

    public StudentController(ImportCsvDbContext csvContext)
    {
        _csvContext = csvContext;
    }
    
    [HttpGet]
    public IActionResult StudentForm()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SubmitStudentData(SignUpStudent studentData)
    {
        if (ModelState.IsValid)
        {
            // After successful form submission, redirect to the StudentDashboard action
            return RedirectToAction("StudentDashboard", "Student", new { email = studentData.EmailAddress });
        }

        return View("StudentForm", studentData);
    }

    public IActionResult SignUpStudent()
    {
        return View();
    }


    public IActionResult StudentDashboard(string email)
    {
        var reports = _csvContext.CsvImportData
            .Where(r => r.Email == email)
            .ToList();

        var savingsByMonth = reports
            .Where(r => r.SavingsStart.HasValue && r.SavingsBalanceStart.HasValue)
            .GroupBy(r => r.SavingsStart.Value.ToString("MMM"))
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(r => r.SavingsBalanceStart ?? 0)
            }).ToList();

        var sessionDates = reports
            .SelectMany(r => new[] { r.Session2Signup, r.Session3Signup })
            .Where(d => d.HasValue)
            .GroupBy(d => d.Value.ToString("MMM dd"))
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToList();

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

        var viewModel = new StudentDashboardViewModel
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

        return View("~/Views/StudentViews/StudentDashboard.cshtml", viewModel); // ✅ CORRECT MODEL
    }
    public IActionResult Profile()
{
    // Load student profile data
    return View();
}

public IActionResult Survey()
{
    // Load the survey form
    return View();
}
    public IActionResult Success()
    {
        return View();
    }
}
