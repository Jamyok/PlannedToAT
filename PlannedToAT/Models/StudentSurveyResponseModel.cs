using System.Collections.Generic;

namespace PlannedToAT.Models
{
    public class StudentSurveyResponseModel
    {
        public int Id { get; set; } // Unique identifier (optional)
        public string StudentName { get; set; }
        public Dictionary<string, string> Responses { get; set; } // Stores question-response pairs

        public StudentSurveyResponseModel()
        {
            Responses = new Dictionary<string, string>();
        }
    }
}
