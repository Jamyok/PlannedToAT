using Microsoft.AspNetCore.Identity;

namespace PlannedToAT.Data 
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = string.Empty;
    }
}