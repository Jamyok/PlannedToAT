using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;



var builder = WebApplication.CreateBuilder(args);

// db builder for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 32))));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Identity services
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    })

    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();



builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        // Add custom view location
        options.ViewLocationFormats.Add("/Views/StudentViews/{0}.cshtml");
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>(); 
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});




var app = builder.Build();
    app.MapIdentityApi<ApplicationUser>();


// Configure the HTTP request pipeline.
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

app.MapControllers();
//app.MapRazorPages();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});




// Default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Route specifically for admin actions
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
     defaults: new { controller = "AdminInput" })
    .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });//require admin 
app.MapIdentityApi<PlannedToAT.Models.AdminUser>();

// Route specifically for Student actions
app.MapControllerRoute(
    name: "student",
    pattern: "Student/{action=Index}/{id?}",
     defaults: new { controller = "StudentInput" })
    .RequireAuthorization(new AuthorizeAttribute { Roles = "StudentUser" });//require Student

// Call the SeedData method to ensure roles and admin user are created


using (var scope = app.Services.CreateScope())
    {
       await RoleInitializer.SeedRolesAndAdminAsync(scope.ServiceProvider); // your usual code
    }


app.Run();


//create first admin user
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
