using DotnetApiPostgres.Api;
using DotnetApiPostgres.Api.Extensions;
using DotnetApiPostgres.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(connectionString));

builder.Services.AddTransient<IPersonService, PersonService>();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok("Hello...");
}
);

// seeding the database
await app.InitializedAsync();

app.Run();

