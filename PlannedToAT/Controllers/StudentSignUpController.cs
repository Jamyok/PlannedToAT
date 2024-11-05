using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;

public class StudentController : Controller
{
    [HttpGet]
    public IActionResult StudentForm()
    {
        return View();
    }

    [HttpPost]
public IActionResult SubmitStudentData(SignUpStudent studentData)
{
    if (ModelState.IsValid)
    {
        return RedirectToAction("StudentDashboard", "Home", new { studentName = studentData.StudentName, dob = studentData.DateOfBirth, race = studentData.RaceEthnicity, phone = studentData.PhoneNumber, email = studentData.EmailAddress, institution = studentData.Institution, subgroup = studentData.SubgroupOrTeam });
    }

    return View("StudentForm", studentData);
}


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

        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
