using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PlannedToAT.Models
{
    public class StudentSurveyModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please share your experience in the program")]
        [DataType(DataType.MultilineText)]
        public string? ProgramExperience { get; set; }

        [Required(ErrorMessage = "Please rate your satisfaction")]
        public string? Satisfaction { get; set; }

        public List<SelectListItem>? UsefulTopics { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Budgeting", Value = "Budgeting" },
            new SelectListItem { Text = "Saving Money", Value = "Saving Money" },
            new SelectListItem { Text = "Investing Basics", Value = "Investing Basics" },
            new SelectListItem { Text = "Credit Scores & Loans", Value = "Credit Scores & Loans" },
            new SelectListItem { Text = "Debt Management", Value = "Debt Management" }
        };

        [Required(ErrorMessage = "Please rate your confidence level in financial literacy")]
        [Range(1, 10, ErrorMessage = "Confidence level must be between 1 and 10")]
        public int ConfidenceLevel { get; set; }

        [Required(ErrorMessage = "Please indicate how your peer interactions were")]
        public string? PeerInteraction { get; set; }

        [Required(ErrorMessage = "Please indicate how your mentor interactions were")]
        public string? MentorInteraction { get; set; }

        [DataType(DataType.MultilineText)]
        public string? MentorshipExperience { get; set; }

        [Required(ErrorMessage = "Would you recommend this program?")]
        public string? WouldRecommend { get; set; }
    }
}
