using Url_Shortener.Data.DTO;

namespace Url_Shortener.Services;

public interface IUrlOperations
{
    Task<string> LongToShortUrlAsync(LongRequest request);
    Task<string> ShortToLongUrlAsync(ShortRequest request);
}
