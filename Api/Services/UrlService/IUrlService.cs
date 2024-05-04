using Url_Shortener.Data.DTO;

namespace Url_Shortener.Services.UrlService;

public interface IUrlService
{
    Task<string> LongToShortUrlAsync(LongRequest request);
    Task<string> ShortToLongUrlAsync(ShortRequest request);
}
