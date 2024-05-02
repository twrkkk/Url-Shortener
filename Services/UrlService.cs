using Microsoft.EntityFrameworkCore;
using Url_Shortener.Data.Context;
using Url_Shortener.Data.DTO;
using Url_Shortener.Data.Entities;

namespace Url_Shortener.Services;

public class UrlService : IUrlOperations
{
    const string alphabet = "ABCDEFGHIJKLMNOPRSTUVWXYQ1234567890@abcdefghijklmnoprstuvWxyq";
    private readonly MainDbContext _db;

    public UrlService(MainDbContext db)
    {
        _db = db;
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

        _db.Urls.Add(urlEntity);
        await _db.SaveChangesAsync();

        return randomStr;
    }

    public async Task<string> ShortToLongUrlAsync(ShortRequest request)
    {
        var data = await _db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == request.ShortUrl);
        return data.Url;
    }
}
