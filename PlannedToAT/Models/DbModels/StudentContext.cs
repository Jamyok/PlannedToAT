using Microsoft.EntityFrameworkCore;

namespace PlannedToAT
{
    public class StudentContext(DbContextOptions<StudentContext> options) : DbContext(options)
    {

        // DbSet for the Students table
        public required DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table name and column mappings (optional)
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students"); // Maps to "Students" table in the database

                entity.Property(e => e.StudentName)
                      .HasColumnName("Student_name") // Maps property to the column "Student_name"
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Age)
                      .HasColumnName("Age");

                entity.Property(e => e.SavingsAmount)
                      .HasColumnName("Savings_amount")
                      .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EntryDate)
                      .HasColumnName("Entery_date")
                      .HasColumnType("datetime");
            });
        }
    }
}
