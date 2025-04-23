using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LoadCsv.Models;

namespace LoadCsv;
public class ImportCsvDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<ReportsModel> CsvImportData { get; set; }

    public ImportCsvDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }
}