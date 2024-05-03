namespace Url_Shortener.Data.Entities
{
    public class UrlDB : BaseEntity
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}
