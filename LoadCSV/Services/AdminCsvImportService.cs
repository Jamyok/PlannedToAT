using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using LoadCsv.Models;

namespace LoadCsv.Services
{
    public class AdminCsvImportService
    {
        private readonly ImportCsvDbContext _context;

        public AdminCsvImportService(ImportCsvDbContext context)
        {
            _context = context;
        }

        public void ImportCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV not found.", filePath);

            var students = new List<ReportsModel>();
            using var reader = new StreamReader(filePath);
            var headers = reader.ReadLine()?.Split(',');

            while (!reader.EndOfStream)
            {
                var row = reader.ReadLine()?.Split(',');
                if (row == null || row.Length != headers.Length) continue;

                string created = row[2];
                DateTime? parsedCreated = DateTime.TryParseExact(
                    created,
                    new[] { "M/d/yyyy h:mmtt", "M/d/yyyy hh:mmtt", "MM/dd/yyyy h:mmtt", "MM/dd/yyyy hh:mmtt" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeLocal,
                    out DateTime createdDate)
                    ? DateTime.SpecifyKind(createdDate, DateTimeKind.Utc)
                    : (DateTime?)null;

                string checkingStart = row[11]?.Replace("$", "").Replace(",", "").Trim();
                string savingsStart = row[12]?.Replace("$", "").Replace(",", "").Trim();
                string investingStart = row[13]?.Replace("$", "").Replace(",", "").Trim();

                var student = new ReportsModel
                {
                    ParticipantID = int.TryParse(row[0], out var id) ? id : 0,
                    FullName = row[1],
                    Created = parsedCreated,
                    FirstName = row[3],
                    LastName = row[4],
                    Email = row[5],
                    PhoneNumber = row[6],
                    DOB = DateTime.TryParseExact(row[7], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dobDate) ? dobDate : (DateTime?)null,
                    Cohorts = row[8],
                    PhotoPermission = !string.IsNullOrWhiteSpace(row[9]),
                    Accounts = row[10],
                    ExitTickets = row[14],
                    NeedsWants = row[15],
                    SMARTGoal = row[16],
                    FamilyFriends = row[17],
                    SavingGoal = row[18],
                    Session2Signup = DateTime.TryParseExact(row[19], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var s2) ? s2 : (DateTime?)null,
                    Session3Signup = DateTime.TryParseExact(row[20], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var s3) ? s3 : (DateTime?)null,
                    CheckingBalanceStart = decimal.TryParse(checkingStart, out var cb) ? cb : (decimal?)null,
                    SavingsBalanceStart = decimal.TryParse(savingsStart, out var sb) ? sb : (decimal?)null,
                    InvestingBalanceStart = decimal.TryParse(investingStart, out var ib) ? ib : (decimal?)null,
                    State = row[24]
                };

                students.Add(student);
            }

            _context.CsvImportData.RemoveRange(_context.CsvImportData);
            _context.SaveChanges();

            _context.CsvImportData.AddRange(students);
            _context.SaveChanges();
        }
    }
}