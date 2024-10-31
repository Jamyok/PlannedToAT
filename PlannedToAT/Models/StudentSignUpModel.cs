using System;
using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models
{
    public class SignUpStudent
    {
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

        public string? SubgroupOrTeam { get; set; }

         public string? ESig { get; set; }
    }
}