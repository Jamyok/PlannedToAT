using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(100)] // Adjust as needed for your database column size
        public required string StudentName { get; set; } // Maps to Student_name

        [Range(1, 150)] // Optional: Validates age range
        public int Age { get; set; } // Maps to Age

        [Column(TypeName = "decimal(18, 2)")] // Matches typical SQL representation for currency/amounts
        public decimal SavingsAmount { get; set; } // Maps to Savings_amount

        public DateTime EntryDate { get; set; } // Maps to Entery_date
    }
}
