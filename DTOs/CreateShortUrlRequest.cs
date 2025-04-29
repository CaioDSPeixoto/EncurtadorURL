using EncurtadorURL.Models.Enums;

namespace EncurtadorURL.DTOs
{
    public class CreateShortUrlRequest
    {
        public string LongUrl { get; set; } = string.Empty;
        public ExpirationAtEnum ExpirationAt { get; set; }
    }
}
