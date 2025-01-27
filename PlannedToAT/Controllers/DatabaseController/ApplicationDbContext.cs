using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models.StudentModels;

namespace PlannedToAT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // Define DbSet properties for your models
        public DbSet<StudentSignUpModel> SignUpStudent { get; set; }
    }
}
