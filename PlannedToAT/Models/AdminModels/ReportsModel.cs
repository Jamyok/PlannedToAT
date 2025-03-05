using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT.Models.AdminModels
{
    /*public class ReportsModel
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
    }*/

    
    public class ReportsModel
    {
        [Key]
        public int ParticipantID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Created { get; set; }  // Date student was created

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? DOB { get; set; } // Date of birth

        [MaxLength(100)]
        public string Cohorts { get; set; } // Link to Cohorts (kept as string for now)

        public bool PhotoPermission { get; set; } // Convert link to true/false

        [MaxLength(100)]
        public string Accounts { get; set; } // Savings,checking,etc

        public DateTime? CheckingStart { get; set; }
        public DateTime? SavingsStart { get; set; }
        public DateTime? InvestingStart { get; set; }

        [MaxLength(100)]
        public string ExitTickets { get; set; } // Link to exit tickets

        [MaxLength(100)]
        public string NeedsWants { get; set; }

        [MaxLength(100)]
        public string SMARTGoal { get; set; }

        [MaxLength(100)]
        public string FamilyFriends { get; set; }

        [MaxLength(100)]
        public string SavingGoal { get; set; }

        public DateTime? Session2Signup { get; set; }
        public DateTime? Session3Signup { get; set; }

        public decimal? CheckingBalanceStart { get; set; }
        public decimal? SavingsBalanceStart { get; set; }
        public decimal? InvestingBalanceStart { get; set; }

        [MaxLength(50)]
        public string State { get; set; } // State from Cohorts

        public bool? HasBankAccount { get; set; }
        
    }
    
}





























