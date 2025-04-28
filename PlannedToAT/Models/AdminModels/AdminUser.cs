using Microsoft.AspNetCore.Identity;

namespace PlannedToAT.Models
{
    public class AdminUser : IdentityUser
    {
           public string? AdminRole { get; set; } 

    }
}