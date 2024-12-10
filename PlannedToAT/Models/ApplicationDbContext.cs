using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public required DbSet<SignUpStudent> SignUpStudents { get; set; }

        public required DbSet<StudentData> Students { get; set; }
    }
}