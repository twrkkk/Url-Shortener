namespace Url_Shortener.Entities
{
    public class UrlDB: BaseEntity
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}
