using MongoDB.Bson.Serialization.Attributes;

namespace EncurtadorURL.DTOs
{
    public class ShortUrlResponse
    {
        public string ShortUrl { get; set; } = string.Empty;
        public DateTime? ExpirationAt { get; set; }
    }
}
