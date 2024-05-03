using Microsoft.EntityFrameworkCore;
using Url_Shortener.Data.Context;
using Url_Shortener.Data.DTO;
using Url_Shortener.Data.Entities;

namespace Url_Shortener.Services;

public class UrlService : IUrlService
{
    const string alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYQ1234567890@abcdefghijklmnoprstuvWxyq";
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

    public UrlService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<string> LongToShortUrlAsync(LongRequest request)
    {
        var Random = new Random();
        var randomStr = new string(Enumerable.Repeat(alphabet, 7)
            .Select(x => x[Random.Next(x.Length)]).ToArray());

        var urlEntity = new UrlDB
        { 
            Url = request.LongUrl,
            ShortUrl = randomStr
        };

        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Urls.Add(urlEntity);
        await context.SaveChangesAsync();

        return randomStr;
    }

    public async Task<string> ShortToLongUrlAsync(ShortRequest request)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var data = await context.Urls.AsNoTracking().FirstOrDefaultAsync(x => x.ShortUrl == request.ShortUrl);
        return data?.Url;
    }
}
