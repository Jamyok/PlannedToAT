using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<SignUpStudent> SignUpStudents { get; set; }

        public DbSet<StudentData> Students { get; set; }
    }
}