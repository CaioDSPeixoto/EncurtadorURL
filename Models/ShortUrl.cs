using EncurtadorURL.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncurtadorURL.Models
{
    public class ShortUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("shortCode")]
        public string ShortCode { get; set; } = string.Empty;

        [BsonElement("longUrl")]
        public string LongUrl { get; set; } = string.Empty;

        [BsonElement("expiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [BsonElement("expirationType")]
        public ExpirationAtEnum ExpirationAt { get; set; }
    }
}
