using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannedToAT.Models
{
   [Table("studentinputforms")]
    public class SignUpStudent
    {
        [Key]
        public int Id { get; set; } // Assuming the table has a primary key column named Id
        public string? StudentName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? RaceEthnicity { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Institution { get; set; }
        public string? SubgroupOrTeam { get; set; }
        public string? Esig { get; set;}
    }
}