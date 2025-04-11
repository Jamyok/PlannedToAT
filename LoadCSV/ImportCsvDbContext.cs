using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LoadCsv.Models;

namespace LoadCsv;
public class ImportCsvDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<ImportDataModel> CsvImportData { get; set; }

    public ImportCsvDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine("Connected with "+ connectionString);
        optionsBuilder.UseNpgsql("host=localhost;user id=postgres;password=abc123;port=5432;database=plannedtoat;");
    }
}