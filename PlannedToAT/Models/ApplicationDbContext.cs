using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Data;
using StudentManagementApp.Models;


namespace PlannedToAT.Models
{
      public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        // Existing DbSet properties
        public DbSet<SignUpStudent> SignUpStudents { get; set; }
        public DbSet<StudentData> Students { get; set; }

        // Add AdminInputFormModel DbSet

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();

    }

    // Your existing DbSets
    public DbSet<SignUpStudent> SignUpStudents { get; set; }

    public DbSet<StudentData> Students { get; set; }

}
}