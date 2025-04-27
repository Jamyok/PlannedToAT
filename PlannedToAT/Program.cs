using Microsoft.EntityFrameworkCore;
using PlannedToAT.Models;
using LoadCsv.Services;
using LoadCsv;

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

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/StudentViews/{0}.cshtml");
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
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Index}/{id?}",
    defaults: new { controller = "AdminInput" });

app.Run();
