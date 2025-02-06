using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models.AdminModels
{
    public class AdminInputFormModel
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Security Question")]
        public string? SecurityQuestion { get; set; }

        [Required]
        [Display(Name = "Security Answer")]
        public string? SecurityAnswer { get; set; }
    }
}