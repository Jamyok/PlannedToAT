using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using System.Collections.Generic;

namespace PlannedToAT.Controllers
{
    public class StudentSurveyController : Controller
    {
        private static SurveyManagementModel _currentSurvey = new SurveyManagementModel
        {
            SurveyTitle = "Student Feedback Survey",
            Questions = new List<SurveyQuestion>
            {
                new SurveyQuestion { Text = "How would you rate the program?", Type = "Radio", Options = "Excellent,Good,Neutral,Poor" },
                new SurveyQuestion { Text = "What did you find most valuable?", Type = "Textarea" },
                new SurveyQuestion { Text = "Would you recommend this program?", Type = "Radio", Options = "Yes,No" }
            }
        };

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSurvey(SurveyManagementModel model)
        {
            if (model == null)
            {
                return NotFound("Survey data not provided.");
            }

            // Update the static survey model
            _currentSurvey = model;

            // Redirect to a success page
            return RedirectToAction("SurveyUpdateSuccess", "AdminInput");
        }

        public IActionResult Index()
        {
            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", _currentSurvey);
        }

        // Display the updated student survey
        public IActionResult ViewUpdatedSurvey()
        {
            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", _currentSurvey);
        }

        // Handle student survey submissions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(StudentSurveyResponseModel response)
        {
            if (ModelState.IsValid)
            {
                // Logic to save student responses (e.g., database storage, logging, etc.)
                return RedirectToAction("SurveySubmitted");
            }

            return View("~/Views/StudentSurvey/SurveySuccess.cshtml", _currentSurvey);
        }

        // Success page after submission
        public IActionResult SurveySubmitted()
        {
            return View("~/Views/StudentSurvey/SurveySuccess.cshtml");
        }
    }
}
