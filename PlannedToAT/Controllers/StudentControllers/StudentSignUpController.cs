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
          
            return RedirectToAction("Success"); 
        }

        // If there are validation errors, return to the form
        return View("StudentForm", studentData);
    }

    public IActionResult Success()
    {
        return View();
    }
}