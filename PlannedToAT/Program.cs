using Microsoft.EntityFrameworkCore;
using PlannedToAT;
using PlannedToAT.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


/*builder.Services.AddDbContext<AdminStudentDataModel>(options =>
{
    options.UseMySql(
        "server=localhost;database=plannedtoat;user=root;password=password123;port=3306;",
        new MySqlServerVersion(new Version(8, 0, 40)
        )
    );
});*/

builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseMySQL("server=localhost;database=plannedtoat;user=root;password=password123")); // Replace with your connection string name

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

app.UseAuthorization();

// Default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Route specifically for admin actions
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "AdminInput" });

app.MapControllerRoute(
    name: "reports",
    pattern: "Admin/Reports",
    defaults: new { controller = "Report", action = "Reports" }
);

app.Run();
