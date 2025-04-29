using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LoadCsv.Models;
using Microsoft.VisualBasic.FileIO;

namespace LoadCsv.Services
{
    public class CsvImportService
    {
        private readonly ImportCsvDbContext _context;

        public CsvImportService(ImportCsvDbContext context)
        {
            _context = context;
        }

        public void ImportCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV not found.", filePath);

            var students = new List<ImportDataModel>();
            var dateFormats = new[] {
                "MM/dd/yyyy", "M/d/yyyy",
                "MM/dd/yyyy h:mmtt", "M/d/yyyy h:mmtt",
                "MM/dd/yyyy hh:mm tt", "M/d/yyyy hh:mm tt"
            };

            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip headers
                if (!parser.EndOfData)
                    parser.ReadFields();

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    if (fields == null || fields.Length < 24) continue; // Allow 24+ because HasBankAccount is derived

                    var student = new ImportDataModel
                    {
                        ParticipantID = int.TryParse(CleanValue(fields[0]), out int id) ? id : 0,
                        FullName = CleanValue(fields[1]),
                        Created = TryParseDate(CleanValue(fields[2]), dateFormats),
                        FirstName = CleanValue(fields[3]),
                        LastName = CleanValue(fields[4]),
                        Email = CleanValue(fields[5]),
                        PhoneNumber = CleanValue(fields[6]),
                        DOB = TryParseDate(CleanValue(fields[7]), dateFormats),
                        Cohorts = CleanValue(fields[8], "Unknown"),
                        PhotoPermission = CleanValue(fields[9]),
                        Accounts = CleanValue(fields[10]),
                        CheckingStartImage = CleanValue(fields[11]),
                        SavingsStartImage = CleanValue(fields[12]),
                        InvestingStartImage = CleanValue(fields[13]),
                        ExitTickets = CleanValue(fields[14], "N/A"),
                        NeedsWants = CleanValue(fields[15]),
                        SMARTGoal = CleanValue(fields[16]),
                        FamilyFriends = CleanValue(fields[17], "N/A"),
                        SavingGoal = CleanValue(fields[18]),
                        Session2Signup = TryParseDate(CleanValue(fields[19]), dateFormats),
                        Session3Signup = TryParseDate(CleanValue(fields[20]), dateFormats),
                        CheckingBalanceStart = TryParseDecimal(CleanValue(fields[21])),
                        SavingsBalanceStart = TryParseDecimal(CleanValue(fields[22])),
                        InvestingBalanceStart = TryParseDecimal(CleanValue(fields[23])),
                        State = CleanValue(fields.Length > 24 ? fields[24] : "", "Unknown"),
                        HasBankAccount = !string.IsNullOrWhiteSpace(CleanValue(fields[10]))
                    };

                    students.Add(student);
                }
            }

            _context.CsvImportData.AddRange(students);
            _context.SaveChanges();
        }

        private static string CleanValue(string? input, string fallback = "N/A") =>
            string.IsNullOrWhiteSpace(input) ? fallback : input.Trim();

        private static DateTime? TryParseDate(string input, string[] formats)
        {
            if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime date))
            {
                return DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
            return null;
        }

        private static decimal? TryParseDecimal(string input)
        {
            if (decimal.TryParse(input?.Replace("$", "").Trim(), out decimal result))
            {
                return result;
            }
            return null;
        }
    }
}
