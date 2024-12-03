using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT.Models
{
    public class SignUpStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [Required(ErrorMessage = "Name is required")]
        public string? StudentName { get; set; } // Nullable to allow for uninitialized state

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Race/Ethnicity is required")]
        public string? RaceEthnicity { get; set; } // Nullable to avoid constructor warning

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Institution/School/Youth Group is required")]
        public string? Institution { get; set; } // Nullable to avoid constructor warning
         
         [Required(ErrorMessage = "Esig is required")]
        public string? Esig { get; set; }

        public string? SubgroupOrTeam { get; set; }
    }
}