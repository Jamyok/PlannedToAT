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
                    question.Options = "";
                }
            }

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
            };

            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", studentSurvey);
        }

        public IActionResult ViewUpdatedSurvey()
        {
            return View("~/Views/StudentSurvey/DynamicStudentSurvey.cshtml", _currentSurvey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(StudentSurveyAnswers response)
        {
            if (response == null || response.Responses == null || response.Responses.Count == 0)
            {
                return BadRequest("No responses provided.");
            }

            string studentName = Request.Form["StudentName"];
            string studentEmail = Request.Form["Email"];

            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentEmail))
            {
                return BadRequest("Name and Email are required.");
            }

            var survey = new SurveyManagementModel
            {
                StudentName = studentName,
                Email = studentEmail,
                ProgramExperience = response.Responses.ContainsKey("Program Experience") ? response.Responses["Program Experience"] : null,
                Satisfaction = response.Responses.ContainsKey("Satisfaction") ? response.Responses["Satisfaction"] : "",
                SurveyTitle = _currentSurvey.SurveyTitle
            };

            dbContext.Surveys.Add(survey);
            dbContext.SaveChanges();

            foreach (var entry in response.Responses)
            {
                var surveyResponse = new StudentSurveyResponseModel
                {
                    StudentEmail = studentEmail,
                    StudentName = studentName,
                    Question = entry.Key,
                    Response = entry.Value
                };

                dbContext.StudentSurvey.Add(surveyResponse);
            }

            dbContext.SaveChanges();

            return RedirectToAction("SurveySuccess");
        }

        public IActionResult SurveySuccess()
        {
            return View("~/Views/StudentSurvey/SurveySuccess.cshtml");
        }
    }
}