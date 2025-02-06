using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models.AdminModels;

namespace PlannedToAT.Models
{
    public class AdminDbContext : DbContext
    {
        public DbSet<ReportData> ReportData { get; set; }

        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;database=plannedtoat;user=root;password=password123");
            }
        }*/

        

        
    }
}
