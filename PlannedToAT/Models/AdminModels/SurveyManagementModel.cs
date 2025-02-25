using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models
{
    public class SurveyManagementModel
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string SurveyTitle { get; set; }
        public List<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
    }

    public class SurveyQuestion
    {
        [Key] 
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Question Type")]
        public string Type { get; set; } // Text, Textarea, Radio, Checkbox

        [Required]
        [Display(Name = "Options (for multiple choice only)")]
        public string Options { get; set; } // Comma-separated for multiple-choice
    }
}
