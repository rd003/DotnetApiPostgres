using DotnetApiPostgres.Api;
using DotnetApiPostgres.Api.Models;
using DotnetApiPostgres.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(connectionString));

builder.Services.AddTransient<IPersonService, PersonService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

