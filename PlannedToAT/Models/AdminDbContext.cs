using Microsoft.EntityFrameworkCore;

namespace PlannedToAT.Models
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options) { }

        // Map the AdminInputFormModel to a database table
        public DbSet<AdminInputFormModel> AdminInputForms { get; set; }
    }
}