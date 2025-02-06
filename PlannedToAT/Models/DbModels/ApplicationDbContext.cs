using Microsoft.EntityFrameworkCore;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }


        public DbSet<SignUpStudent> SignUpStudents { get; set; }

        //public DbSet<StudentPersonalData> StudentPersonalData { get; set; }
        
    }
}