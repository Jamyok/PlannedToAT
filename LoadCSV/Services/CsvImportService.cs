using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LoadCsv.Models;

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

            using (var reader = new StreamReader(filePath))
            {
                var headers = reader.ReadLine()?.Split(',');

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine()?.Split(',');

                    if (row == null || row.Length < 25) continue;

                    var student = new ImportDataModel
                    {
                        ParticipantID = int.TryParse(CleanValue(row[0]), out int id) ? id : 0,
                        FullName = CleanValue(row[1]),
                        Created = TryParseDate(CleanValue(row[2]), dateFormats),
                        FirstName = CleanValue(row[3]),
                        LastName = CleanValue(row[4]),
                        Email = CleanValue(row[5]),
                        PhoneNumber = CleanValue(row[6]),
                        DOB = TryParseDate(CleanValue(row[7]), dateFormats),
                        Cohorts = CleanValue(row[8], "Unknown"),
                        PhotoPermission = CleanValue(row[9]),
                        Accounts = CleanValue(row[10]),
                        CheckingStartImage = CleanValue(row[11]),
                        SavingsStartImage = CleanValue(row[12]),
                        InvestingStartImage = CleanValue(row[13]),
                        ExitTickets = CleanValue(row[14], "N/A"),
                        NeedsWants = CleanValue(row[15]),
                        SMARTGoal = CleanValue(row[16]),
                        FamilyFriends = CleanValue(row[17], "N/A"),
                        SavingGoal = CleanValue(row[18]),
                        Session2Signup = TryParseDate(CleanValue(row[19]), dateFormats),
                        Session3Signup = TryParseDate(CleanValue(row[20]), dateFormats),
                        CheckingBalanceStart = TryParseDecimal(CleanValue(row[21])),
                        SavingsBalanceStart = TryParseDecimal(CleanValue(row[22])),
                        InvestingBalanceStart = TryParseDecimal(CleanValue(row[23])),
                        State = CleanValue(row[24], "Unknown"),
                        HasBankAccount = !string.IsNullOrWhiteSpace(CleanValue(row[10]))
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
