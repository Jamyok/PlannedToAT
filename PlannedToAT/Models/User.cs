using Microsoft.AspNetCore.Identity;

namespace PlannedToAT.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = string.Empty;
    }
}