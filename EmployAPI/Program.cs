using Microsoft.EntityFrameworkCore;
using EmployAPI.Data;
using EmployAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//Use conenction string from appsettings.json
builder.Services.AddDbContext<EmployContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployDbCS")));

var app = builder.Build();

//Asynchronously method to seed data into our database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<EmployContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error has occured while migrating the database: {ex.Message}");
    }
}
app.MapGet("/", () => "Hello World!");
app.MapEmployEndpoints();

app.Run();

