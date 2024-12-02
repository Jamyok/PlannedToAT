using PlannedToAT.Models;
using Microsoft.EntityFrameworkCore;

namespace PlannedToAT;

    public class StudentContext: ApplicationDbContext
    {
        public DbSet<int> id {get; set; }

        public StudentContext(DbContextOptions options) : base(options)
        {

        }

    }