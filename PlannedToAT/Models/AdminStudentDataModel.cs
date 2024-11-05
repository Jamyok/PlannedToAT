using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models
{
    public class StudentData
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string School { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        public int GraduatingYear { get; set; }

        public bool HasBankAccount { get; set; } = false;

        [StringLength(100)]
        public string Placeholder1 { get; set; } = string.Empty;

        [StringLength(100)]
        public string Placeholder2 { get; set; } = string.Empty;

        public StudentData()
        {
            //left empty for now
        }
    }
}