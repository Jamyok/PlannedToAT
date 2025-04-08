using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Data;
using PlannedToAT.Models.AdminModels;
using PlannedToAT.Models.StudentModels;
using StudentManagementApp.Models;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // DbSet properties for your models
        public DbSet<SignUpStudent> SignUpStudents { get; set; }
        public DbSet<StudentData> Students { get; set; }
        public DbSet<SurveyManagementModel> Surveys { get; set; }

        public DbSet<AdminInputFormModel> AdminSignUp { get; set; }

        public DbSet<StudentSurveyResponseModel> StudentSurvey { get; set; }

        public DbSet<ReportsModel> ReportData { get; set;} 

        // Constructor with DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Factory method to create an instance (if needed)
        public static ApplicationDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Configure your options here if needed
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
