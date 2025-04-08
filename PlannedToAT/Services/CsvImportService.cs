using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;
using PlannedToAT.Models.AdminModels;

namespace PlannedToAT.Services
{
    public class CsvImportService
    {
        private readonly ApplicationDbContext _context;

        public CsvImportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ImportCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("CSV not found.", filePath);
            }

            var students = new List<ReportsModel>();

            using (var reader = new StreamReader(filePath))
            {
                var headers = reader.ReadLine()?.Split(',');

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine()?.Split(',');

                    if (row == null || row.Length != headers.Length) continue;

                    // Read all values as strings first to handle nulls
                    string participantId = row[0];
                    string fullName = row[1];
                    string created = row[2];
                    string firstName = row[3];
                    string lastName = row[4];
                    string email = row[5];
                    string phoneNumber = row[6];
                    string dob = row[7];
                    string cohorts = row[8];
                    string photoPermission = row[9];
                    string accounts = row[10];
                    string checkingStart = row[11];
                    string savingsStart = row[12];
                    string investingStart = row[13];
                    string exitTickets = row[14];
                    string needsWants = row[15];
                    string smartGoal = row[16];
                    string familyFriends = row[17];
                    string savingGoal = row[18];
                    string session2Signup = row[19];
                    string session3Signup = row[20];
                    string checkingBalanceStart = row[21];
                    string savingsBalanceStart = row[22];
                    string investingBalanceStart = row[23];
                    string state = row[24];

                    var student = new ReportsModel
                    {
                        ParticipantID = int.TryParse(participantId, out int id) ? id : 0,
                        FullName = fullName,
                        Created = DateTime.TryParseExact(created, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime createdDate) ? createdDate : (DateTime?)null,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        DOB = DateTime.TryParseExact(dob, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dobDate) ? dobDate : (DateTime?)null,
                        Cohorts = cohorts,
                        PhotoPermission = !string.IsNullOrEmpty(photoPermission), // If string exists, it's true
                        Accounts = accounts,
                        CheckingStart = DateTime.TryParseExact(checkingStart, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime checkingDate) ? checkingDate : (DateTime?)null,
                        SavingsStart = DateTime.TryParseExact(savingsStart, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime savingsDate) ? savingsDate : (DateTime?)null,
                        InvestingStart = DateTime.TryParseExact(investingStart, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime investingDate) ? investingDate : (DateTime?)null,
                        ExitTickets = exitTickets,
                        NeedsWants = needsWants,
                        SMARTGoal = smartGoal,
                        FamilyFriends = familyFriends,
                        SavingGoal = savingGoal,
                        Session2Signup = DateTime.TryParseExact(session2Signup, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime session2Date) ? session2Date : (DateTime?)null,
                        Session3Signup = DateTime.TryParseExact(session3Signup, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime session3Date) ? session3Date : (DateTime?)null,
                        CheckingBalanceStart = decimal.TryParse(checkingBalanceStart, out decimal checkingBal) ? checkingBal : (decimal?)null,
                        SavingsBalanceStart = decimal.TryParse(savingsBalanceStart, out decimal savingsBal) ? savingsBal : (decimal?)null,
                        InvestingBalanceStart = decimal.TryParse(investingBalanceStart, out decimal investingBal) ? investingBal : (decimal?)null,
                        State = state
                    };

                    students.Add(student);
                }
            }

            _context.ReportData.AddRange(students);
            _context.SaveChanges();
        }
    }
}
