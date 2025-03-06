using Microsoft.AspNetCore.Identity;

namespace PlannedToAT.Data 
{
    public class StudentUser : IdentityUser
    {
    public string StudentName { get; set; } // Ensure you have this property
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string? Institution { get; set; }
    public string? SubgroupOrTeam { get; set; }
    }
}