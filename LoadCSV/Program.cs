// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        private readonly ApplicationDbContext _context;
        private readonly CsvImportService _csvImportService;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            _csvImportService = new CsvImportService(_context);
            _csvImportService.ImportCsv("wwwroot/data/Participants-All_data_fields.csv"); 
        }
    }
}