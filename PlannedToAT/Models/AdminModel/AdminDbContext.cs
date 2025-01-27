using Microsoft.EntityFrameworkCore;

namespace PlannedToAT.Models
{
    public class AdminDbContext : DbContext
    {
        public DbSet<AdminInputFormModel> AdminInputForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=plannedtoat;user=root;password=Moses.123");
            //optionsBuilder.UseMySQL("server=plannedtoatscus.mysql.database.azure.com;database=plannedtoat;user=Jamy;password=Faye2011");
        }
    }
}