using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Required for TempData serialization
using PlannedToAT.Models.StudentModels;

public class StudentController : Controller
{
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
            TempData["StudentData"] = JsonConvert.SerializeObject(studentData);
            return RedirectToAction("StudentDashboard");
        }

        return View("StudentForm", studentData);
    }

    public IActionResult SignUpStudent()
    {
        return View();
    }

    public IActionResult StudentDashboard()
    {
        if (TempData["StudentData"] is string studentJson)
        {
            var model = JsonConvert.DeserializeObject<SignUpStudent>(studentJson);
            return View("~/Views/StudentViews/StudentDashboard.cshtml", model);
        }

        return RedirectToAction("StudentForm");
    }

    public IActionResult Success()
    {
        return View();
    }
}
