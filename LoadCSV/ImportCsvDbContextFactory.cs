using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LoadCsv
{
    public class ImportCsvDbContextFactory : IDesignTimeDbContextFactory<ImportCsvDbContext>
    {
        public ImportCsvDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            return new ImportCsvDbContext(configuration);
        }
    }
}
