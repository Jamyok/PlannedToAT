using System.ComponentModel.DataAnnotations;

namespace PlannedToAT.Models.ReportsModels
{
    public class ReportsModel
    {
        [Key]
        public int ParticipantID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Created { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? DOB { get; set; }

        [MaxLength(100)]
        public string Cohorts { get; set; }

        [MaxLength(1000)]
        public string PhotoPermission { get; set; }

        [MaxLength(1000)]
        public string Accounts { get; set; }

        [MaxLength(1000)]
        public string CheckingStartImage { get; set; }

        [MaxLength(1000)]
        public string SavingsStartImage { get; set; }

        [MaxLength(1000)]
        public string InvestingStartImage { get; set; }

        [MaxLength(1000)]
        public string ExitTickets { get; set; }

        [MaxLength(500)]
        public string NeedsWants { get; set; }

        [MaxLength(500)]
        public string SMARTGoal { get; set; }

        [MaxLength(1000)]
        public string FamilyFriends { get; set; }

        [MaxLength(500)]
        public string SavingGoal { get; set; }

        public DateTime? Session2Signup { get; set; }

        public DateTime? Session3Signup { get; set; }

        public decimal? CheckingBalanceStart { get; set; }

        public decimal? SavingsBalanceStart { get; set; }

        public decimal? InvestingBalanceStart { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        public bool? HasBankAccount { get; set; }
    }
}
