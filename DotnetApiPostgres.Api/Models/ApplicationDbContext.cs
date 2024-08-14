using DotnetApiPostgres.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApiPostgres.Api;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }
}