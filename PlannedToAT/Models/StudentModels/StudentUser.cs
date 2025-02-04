using Microsoft.AspNetCore.Identity;

namespace PlannedToAT.Data 
{
    public class StudentUser : IdentityUser
    {

    public string? Institution { get; set; }
    public string? SubgroupOrTeam { get; set; }
    }
}