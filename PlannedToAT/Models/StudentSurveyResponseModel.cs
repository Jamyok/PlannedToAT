using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models
{
    public class StudentSurveyResponseModel
    {
        [Key]
        public int Id { get; set; } // Unique identifier (optional)
        [Required]
        public string StudentEmail { get; set; }

        [Required]
        public string Question { get; set; } 

        [Required]
        public string Response { get; set; } // Stores question-response pairs

    }
    public class StudentSurveyAnswers
    {
        public Dictionary<string, string> Responses { get; set; }

    }
}
