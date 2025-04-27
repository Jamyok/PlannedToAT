using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoadCSV.Migrations
{
    /// <inheritdoc />
    public partial class AddCsvImportDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CsvImportData",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cohorts = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhotoPermission = table.Column<bool>(type: "boolean", nullable: false),
                    Accounts = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CheckingStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SavingsStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InvestingStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExitTickets = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NeedsWants = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SMARTGoal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FamilyFriends = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SavingGoal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Session2Signup = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Session3Signup = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CheckingBalanceStart = table.Column<decimal>(type: "numeric", nullable: true),
                    SavingsBalanceStart = table.Column<decimal>(type: "numeric", nullable: true),
                    InvestingBalanceStart = table.Column<decimal>(type: "numeric", nullable: true),
                    State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HasBankAccount = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsvImportData", x => x.ParticipantID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CsvImportData");
        }
    }
}
