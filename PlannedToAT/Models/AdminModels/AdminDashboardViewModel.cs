namespace PlannedToAT.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<string> States { get; set; }
        public List<int> UsersPerState { get; set; }

        public List<string> Months { get; set; }
        public List<int> SignupsPerMonth { get; set; }

        public decimal AvgChecking { get; set; }
        public decimal AvgSavings { get; set; }
        public decimal AvgInvesting { get; set; }

        public List<string> SessionSignupDates { get; set; }
        public List<int> SignupCounts { get; set; }

        public int CompletedForms { get; set; }
        public int InProgressForms { get; set; }
        public int NotStartedForms { get; set; }

        public string FirstName { get; set; } 

        
    }
}
