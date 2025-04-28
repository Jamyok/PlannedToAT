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
    }
}
