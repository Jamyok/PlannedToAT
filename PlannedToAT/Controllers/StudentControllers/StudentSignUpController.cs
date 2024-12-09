using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;

public class StudentController : Controller
{
    [HttpGet]
    public IActionResult StudentDashboard()
    {
        return View("/Views/StudentViews/StudentDashboard.cshtml");
    }

    [HttpPost]
    public IActionResult SubmitStudentData(SignUpStudent studentData)
    {
        if (ModelState.IsValid)
        {
          
            return RedirectToAction("Success"); 
        }

        // If there are validation errors, return to the form
        return View("/Views/StudentViews/StudentDashboard.cshtml", studentData);
    }

    public IActionResult Success()
    {
        return View();
    }
}