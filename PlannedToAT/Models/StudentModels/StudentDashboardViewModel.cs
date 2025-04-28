namespace PlannedToAT.ViewModels
{
    public class StudentDashboardViewModel
    {
        public string? StudentName { get; set; }

        public List<string> SavingsMonths { get; set; } = new();
        public List<int> MonthlySavings { get; set; } = new();

        public List<string> FormDueDates { get; set; } = new();
        public List<int> FormsDue { get; set; } = new();

        public int CompletedCount { get; set; }
        public int InProgressCount { get; set; }
        public int NotStartedCount { get; set; }
 
        public decimal? CheckingBalanceStart { get; set; }
        public decimal? SavingsBalanceStart { get; set; }
        public decimal? InvestingBalanceStart { get; set; }
        public decimal? AvgCheckingBalance { get; set; }
        public decimal? AvgSavingsBalance { get; set; }
        public decimal? AvgInvestingBalance { get; set; }


        public decimal? AveragePeerChecking { get; set; }
        public decimal? AveragePeerSavings { get; set; }
        public decimal? AveragePeerInvesting { get; set; }

    }
}
