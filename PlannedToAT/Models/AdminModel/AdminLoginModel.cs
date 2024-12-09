using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models
{
    public class AdminLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}