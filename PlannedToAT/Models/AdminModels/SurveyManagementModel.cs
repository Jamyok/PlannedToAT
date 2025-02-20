using System.Collections.Generic;

namespace PlannedToAT.Models
{
    public class SurveyManagementModel
    {
        public string SurveyTitle { get; set; }
        public List<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
    }

    public class SurveyQuestion
    {
        public string Text { get; set; }
        public string Type { get; set; } // Text, Textarea, Radio, Checkbox
        public string Options { get; set; } // Comma-separated for multiple-choice
    }
}
