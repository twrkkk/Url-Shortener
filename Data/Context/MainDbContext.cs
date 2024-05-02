using Microsoft.EntityFrameworkCore;
using Url_Shortener.Data.Entities;

namespace Url_Shortener.Data.Context;

public class MainDbContext: DbContext
{
    public DbSet<UrlDB> Urls { get; set; }

    private readonly ConfigurationManager _configurationManager;

    public MainDbContext(DbContextOptions<MainDbContext> options, ConfigurationManager configurationManager) : base(options)
    {
        _configurationManager = configurationManager;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionSrting = _configurationManager.GetConnectionString("Default");
        optionsBuilder.UseNpgsql(connectionSrting);
    }
}
