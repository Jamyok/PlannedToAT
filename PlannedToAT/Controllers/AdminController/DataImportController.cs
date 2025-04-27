using LoadCsv.Services;
using Microsoft.AspNetCore.Mvc;

namespace PlannedToAT.Controllers
{
    public class DataImportController : Controller
    {
        private readonly CsvImportService _csvImportService;

        [HttpGet]
        public IActionResult ImportFromLocal()
        {
            string filePath = "/Users/namithayadlapalli/PlannedToAT-15/LoadCSV/data/Participants-All_data_fields.csv";
            _csvImportService.ImportCsv(filePath);
            return Ok("Data imported.");
        }
        public DataImportController(CsvImportService csvImportService)
        {
            _csvImportService = csvImportService;
        }

        [HttpPost]
        public IActionResult ImportData(string filePath)
        {
            try
            {
                _csvImportService.ImportCsv(filePath);
                return Ok("Data imported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error importing data: {ex.Message}");
            }
        }
    }
}
