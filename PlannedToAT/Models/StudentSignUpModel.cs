using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT.Models
{
   [Table("studentinputforms")]
    public class SignUpStudent
    {

        [Key] // Defining PhoneNumber as the primary key
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Race/Ethnicity is required")]
        public string? RaceEthnicity { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Institution/School/Youth Group is required")]
        public string? Institution { get; set; }

        public string? SubgroupOrTeam { get; set; }

        public string? ESig { get; set; }

        [Key]
        public int Id { get; set; } // Assuming the table has a primary key column named Id
        public string? StudentName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? RaceEthnicity { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Institution { get; set; }
        public string? SubgroupOrTeam { get; set; }
        public string? Esig { get; set;}
    }
}