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
            return View("~/Views/StudentSurvey/StudentSurvey.cshtml", _currentSurvey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(StudentSurveyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/StudentSurvey/SurveySuccess.cshtml", model);
            }

            string studentEmail = model.Email;

            if (string.IsNullOrEmpty(studentEmail))
            {
                return BadRequest("Unable to retrieve student email. Please provide an email.");
            }

            var responses = new List<StudentSurveyResponseModel>
            {
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Full Name", Response = model.StudentName },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Program Experience", Response = model.ProgramExperience },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Satisfaction", Response = model.Satisfaction },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Confidence Level", Response = model.ConfidenceLevel.ToString() },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Peer Interaction", Response = model.PeerInteraction },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Mentor Interaction", Response = model.MentorInteraction },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Mentorship Experience", Response = model.MentorshipExperience ?? "" },
                new StudentSurveyResponseModel { StudentEmail = studentEmail, Question = "Would Recommend Program", Response = model.WouldRecommend }
            };

            dbContext.StudentSurvey.AddRange(responses);
            dbContext.SaveChanges();

            return RedirectToAction("SurveySuccess");
        }

        public IActionResult SurveySuccess()
        {
            return View("~/Views/StudentSurvey/SurveySuccess.cshtml");
        }
    }
}
