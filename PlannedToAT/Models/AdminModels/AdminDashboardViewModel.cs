namespace PlannedToAT.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<string> Months { get; set; } = new();
        public List<int> TotalStudents { get; set; } = new();

        public int StudentsWithBankAccounts { get; set; }
        public int StudentsWithoutBankAccounts { get; set; }

        public int CompletedForms { get; set; }
        public int InProgressForms { get; set; }
        public int NotStartedForms { get; set; }
    }
}
