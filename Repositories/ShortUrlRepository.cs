using EncurtadorURL.Config;
using EncurtadorURL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EncurtadorURL.Repositories
{
    public class ShortUrlRepository
    {
        private readonly IMongoCollection<ShortUrl> _collection;

        public ShortUrlRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<ShortUrl>(settings.Value.CollectionName);

            IndexTTL();
        }

        /// <summary>
        /// https://www.mongodb.com/pt-br/docs/manual/core/index-ttl/
        /// </summary>
        private void IndexTTL()
        {
            var indexKeys = Builders<ShortUrl>.IndexKeys.Ascending(x => x.ExpiresAt);
            var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
            var indexModel = new CreateIndexModel<ShortUrl>(indexKeys, indexOptions);
            _collection.Indexes.CreateOne(indexModel);
        }

        public async Task<bool> ExistsShortCodeAsync(string shortCode)
        {
            return await _collection.Find(x => x.ShortCode == shortCode).AnyAsync();
        }

        public async Task<ShortUrl> GetByShortCodeAsync(string shortCode)
        {
            return await _collection.Find(x => x.ShortCode == shortCode).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ShortUrl url)
        {
            await _collection.InsertOneAsync(url);
        }
    }
}
