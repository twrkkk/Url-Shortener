using Microsoft.EntityFrameworkCore;
using Url_Shortener.Data.Entities;

namespace Url_Shortener.Data.Context;

public class MainDbContext: DbContext
{
    public DbSet<UrlDB> Urls { get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Default");

        optionsBuilder.UseNpgsql(connectionString);
    }
}
