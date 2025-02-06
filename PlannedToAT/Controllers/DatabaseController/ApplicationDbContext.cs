using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models.StudentModels;

namespace PlannedToAT.Models
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // Define DbSet properties for your models
        public DbSet<StudentSignUpModel> SignUpStudent { get; set; }
    }

    public class StudentSignUpModel
    {
        internal DateTime DateOfBirth;
        internal string StudentName;
        internal string RaceEthnicity;
        internal string PhoneNumber;
        internal string EmailAddress;
        internal string Institution;
        internal string SubgroupOrTeam;
    }
}
