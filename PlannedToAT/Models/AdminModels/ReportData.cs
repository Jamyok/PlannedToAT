/*using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;

namespace PlannedToAT.Models
{
    public class StudentPersonalData
    {
        public int StudentId { get; set;}
        public required string? FirstName { get; set; }
        public required string? LastName {get; set;}
        public required string? School {get; set;}
        public required string? Organization {get; set;}
        public int? GraduatingYear {get; set; }
        public bool? HasBankAccount {get; set;}
    }

    public class ReportData
    {
        public int? TotalStudents { get; set; } // For the TotalStudents report
        public int? StudentsWithBankAccounts { get; set; } // For the StudentsWithBankAccounts report
        public List<StudentPersonalData> StudentDetails { get; set; } // For the StudentDetails report

        public ReportData()
        {
            StudentDetails = new List<StudentPersonalData>();
        }
    }
}*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT.Models.AdminModels
{
    public class ReportData
    {
        [Key]
        [Column("StudentID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string School { get; set; }

        [MaxLength(100)]
        public string Organization { get; set; }

        public int? GraduatingYear { get; set; }

        public bool? HasBankAccount { get; set; }
    }
}
