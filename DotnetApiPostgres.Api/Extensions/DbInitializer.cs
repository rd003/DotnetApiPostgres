using DotnetApiPostgres.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApiPostgres.Api.Extensions;

public static class DbInitializer
{
    public static async Task InitializedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
        }
    }
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        List<Person> people = [
             new Person {Name = "John" },
              new Person {Name = "Max" },
              new Person {Name = "Jill"},
              new Person {Name = "Ken"},
              ];
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("====> Database is created");

        if (await context.People.AnyAsync())
        {
            return;
        }

        context.People.AddRange(people);

        await context.SaveChangesAsync();
        Console.WriteLine("====> Data is seeded");
    }
}
