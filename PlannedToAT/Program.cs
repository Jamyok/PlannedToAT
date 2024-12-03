using Microsoft.EntityFrameworkCore;
using PlannedToAT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Specify the MySQL server version explicitly
builder.Services.AddDbContext<StudentContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=plannedtoat;user=user;password=Faye2011",
        new MySqlServerVersion(new Version(8, 0, 33)) // Replace 8.0.33 with your actual MySQL version
    );
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();