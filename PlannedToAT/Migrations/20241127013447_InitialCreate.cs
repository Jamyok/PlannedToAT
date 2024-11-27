using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PlannedToAT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SignUpStudents",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    StudentName = table.Column<string>(type: "longtext", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RaceEthnicity = table.Column<string>(type: "longtext", nullable: false),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true),
                    Institution = table.Column<string>(type: "longtext", nullable: false),
                    SubgroupOrTeam = table.Column<string>(type: "longtext", nullable: true),
                    ESig = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUpStudents", x => x.PhoneNumber);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    School = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Organization = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    GraduatingYear = table.Column<int>(type: "int", nullable: false),
                    HasBankAccount = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Placeholder1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Placeholder2 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignUpStudents");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
