using Microsoft.AspNetCore.Identity;
using PlannedToAT.Models;

namespace PlannedToAT.Models.AdminModels

{
    public class AdminUser : IdentityUser
    {

           public string? AdminRole { get; set; } //

    }
}