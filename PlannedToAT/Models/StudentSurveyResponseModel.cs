using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models
{
    public class StudentSurveyResponseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudentEmail { get; set; }

        [Required]
        public string StudentName { get; set; } // 🛠 Add this line!

        [Required]
        public string Question { get; set; }

        [Required]
        public string Response { get; set; }
    }

    public class StudentSurveyAnswers
    {
        public Dictionary<string, string> Responses { get; set; }
    }
}
