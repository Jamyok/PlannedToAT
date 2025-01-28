using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models.StudentModels;

namespace PlannedToAT.Models
{
    public class PlannedToATDbContext(DbContextOptions<PlannedToATDbContext> options) : DbContext(options)
    {
        public DbSet<SignUpStudent> StudentInputForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=plannedtoatscus.mysql.database.azure.com;database=plannedtoat;user=Jamy;password=Faye2011",new MySqlServerVersion(new Version(8, 0, 11)));
        }
        
    }
}