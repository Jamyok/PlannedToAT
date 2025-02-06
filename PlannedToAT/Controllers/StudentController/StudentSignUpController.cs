using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using PlannedToAT.Models.StudentModels;

public class StudentController : Controller
{
    [HttpGet]
    public IActionResult StudentForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitStudentData(StudentSignUpModel studentData)
    {
        if (ModelState.IsValid)
        {
            // After successful form submission, redirect to the StudentDashboard action
            return RedirectToAction("StudentDashboard", "Student", new { name = studentData.StudentName, dob = studentData.DateOfBirth, race = studentData.RaceEthnicity, phone = studentData.PhoneNumber, email = studentData.EmailAddress, institution = studentData.Institution, subgroup = studentData.SubgroupOrTeam });
        }

        // Return to the form with data if the model is invalid
        return View("StudentForm", studentData);
    }

    // This method serves the SignUpStudent form, now redirecting to the correct view
    public IActionResult SignUpStudent(StudentSignUpModel model)
    {
        return View("~/Views/StudentViews/SignUpStudent.cshtml");
    }

    // The action that handles displaying the student dashboard
    public IActionResult StudentDashboard(string name, DateTime dob, string race, string phone, string email, string institution, string subgroup)
    {
        var model = new SignUpStudent
        {
            StudentName = name,
            DateOfBirth = dob,
            RaceEthnicity = race,
            PhoneNumber = phone,
            EmailAddress = email,
            Institution = institution,
            SubgroupOrTeam = subgroup
        };
        
        return View("~/Views/StudentViews/StudentDashboard.cshtml", model);
    }
    public IActionResult Success()
    {
        return View();
    }
}
