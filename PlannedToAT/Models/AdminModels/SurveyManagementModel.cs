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
        
        [Required]
        public string Text { get; set; }

        [Required]
        public string Type { get; set; } // Text, Textarea, Radio, Checkbox

        public string Options { get; set; } // Remove `[Required]` so it can be null for non-multiple-choice questions
    }


}
