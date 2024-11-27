using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Existing DbSet properties
        public DbSet<SignUpStudent> SignUpStudents { get; set; }
        public DbSet<StudentData> Students { get; set; }

        // Add AdminInputFormModel DbSet
        public DbSet<AdminInputFormModel> AdminInputForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=plannedtoat;user=root;password=Moses.123");
        }
    }
}