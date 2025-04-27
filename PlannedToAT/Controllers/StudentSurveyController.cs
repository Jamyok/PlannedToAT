using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using System.Collections.Generic;

namespace PlannedToAT.Controllers
{
    [Authorize(Roles = "StudentUser,Admin")]
    public class StudentSurveyController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentSurveyController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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

            foreach (var question in model.Questions)
            {
                if (string.IsNullOrWhiteSpace(question.Options) && (question.Type == "Text" || question.Type == "Textarea"))
                {
                    question.Options = ""; // Ensure it's not null to prevent MySQL errors
                }
            }

            // Update the static survey model
            _currentSurvey = model;

            dbContext.Surveys.Add(model);
            dbContext.SaveChanges();

            return RedirectToAction("SurveyUpdateSuccess", "AdminInput");
        }

        public IActionResult Index()
        {
            var studentSurvey = new StudentSurveyModel
            {
                StudentName = _currentSurvey.StudentName,
                Email = _currentSurvey.Email,
                ProgramExperience = _currentSurvey.ProgramExperience,
                Satisfaction = _currentSurvey.Satisfaction
                // Add others if needed
            };

            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", studentSurvey);

        }

        // Display the updated student survey
        public IActionResult ViewUpdatedSurvey()
        {
            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", _currentSurvey);
        }

        // Handle student survey submissions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(StudentSurveyAnswers response)

        {
            if (response == null || response.Responses == null || response.Responses.Count == 0)
            {
                return BadRequest("No responses provided.");
            }

            // Ensure user is authenticated before saving the survey
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SurveySuccess", "StudentSurvey");
            }

            // Retrieve student email
            string studentEmail = User.Identity.Name;

            // If still null, return an error
            if (string.IsNullOrEmpty(studentEmail))
            {
                return BadRequest("Unable to retrieve student email. Please log in.");
            }

            foreach (var entry in response.Responses)
            {
                var surveyResponse = new StudentSurveyResponseModel
                {
                    StudentEmail = studentEmail,
                    Question = entry.Key,
                    Response = entry.Value
                };

                dbContext.StudentSurvey.Add(surveyResponse);
            }

            dbContext.SaveChanges(); // Persist all responses

            return RedirectToAction("SurveySuccess");
        }

        // Success page after submission
        public IActionResult SurveySuccess()
        {
            return View("~/Views/StudentSurvey/SurveySuccess.cshtml");
        }
    }
}
