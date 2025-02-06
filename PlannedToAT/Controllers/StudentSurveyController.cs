using Microsoft.AspNetCore.Mvc;
using PlannedToAT.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlannedToAT.Controllers
{
    public class StudentSurveyController : Controller
    {
        [HttpGet]
        public IActionResult StudentSurvey()
        {
            return View(new StudentSurveyModel());
        }

        [HttpPost]
        public IActionResult SubmitSurvey(StudentSurveyModel model, List<string> SelectedTopics)
        {
            if (ModelState.IsValid)
            {
                // Store selected topics into model
                model.UsefulTopics = model.UsefulTopics?
                    .Where(t => SelectedTopics.Contains(t.Value))
                    .ToList();

                // Process and save survey responses
                return RedirectToAction("SurveySuccess");
            }

            return View("StudentSurvey", model);
        }

        public IActionResult SurveySuccess()
        {
            return View();
        }
    }
}
