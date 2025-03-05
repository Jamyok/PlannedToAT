using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using PlannedToAT.Data;



var builder = WebApplication.CreateBuilder(args);

// db builder for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 32))));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


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




var app = builder.Build();

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



app.Run();
