using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlannedToAT.Controllers
{
    [Authorize(Roles = "StudentUser")]
    public class StudentSurveyController : Controller
    {
        [HttpGet]
        public IActionResult StudentSurvey()
        {
            return View(new StudentSurveyModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSurvey(StudentSurveyModel model, List<string>? SelectedTopics)
        {
            if (ModelState.IsValid)
            {
                // Ensure SelectedTopics is not null before filtering
                if (SelectedTopics != null && model.UsefulTopics != null)
                {
                    model.UsefulTopics = model.UsefulTopics
                        .Where(t => SelectedTopics.Contains(t.Value))
                        .ToList();
                }

                // Simulating data persistence using TempData (replace with database logic)
                TempData["SurveyData"] = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                return RedirectToAction("SurveySuccess");
            }

            return View("StudentSurvey", model);
        }

        public IActionResult SurveySuccess()
        {
            if (TempData["SurveyData"] is string surveyJson)
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentSurveyModel>(surveyJson);
                return View(model);
            }

            return View();
        }
    }
}
