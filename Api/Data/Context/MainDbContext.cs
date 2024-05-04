using Microsoft.EntityFrameworkCore;
using Url_Shortener.Data.Entities;

namespace Url_Shortener.Data.Context;

public class MainDbContext: DbContext
{
    public DbSet<UrlDB> Urls { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        Database.Migrate();
    }
}
