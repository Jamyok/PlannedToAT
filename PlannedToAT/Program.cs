using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;
using LoadCsv.Services;
using LoadCsv;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

// db builder for PostgreSQL with error resiliency
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    ));

builder.Services.AddDbContext<ImportCsvDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register your CSV import service
builder.Services.AddScoped<CsvImportService>();
builder.Services.AddScoped<AdminCsvImportService>();

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() // ✅ Adds email/phone/2FA token providers
.AddDefaultUI();

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/StudentViews/{0}.cshtml");
    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/AdminInput/Admin"; // or wherever your admin login page is
        options.AccessDeniedPath = "/Home/AccessDenied"; // optional
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Routes

app.UseAuthentication();
app.MapControllers();
app.MapRazorPages(); // ✅ Enables Razor Pages, including Identity UI

// Default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "AdminInput" })
    .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

// Route specifically for Student actions
app.MapControllerRoute(
    name: "student",
    pattern: "Student/{action=Index}/{id?}",
    defaults: new { controller = "StudentInput" })
    .RequireAuthorization(new AuthorizeAttribute { Roles = "StudentUser" });

// Call the SeedData method to ensure roles and admin user are created
using (var scope = app.Services.CreateScope())
{
    await RoleInitializer.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();


// Create first admin user
public static class RoleInitializer
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        Console.WriteLine("Seeding roles and admin user...");
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Ensure Admin Role Exists
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Ensure an Admin User Exists
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin@123"; 

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
