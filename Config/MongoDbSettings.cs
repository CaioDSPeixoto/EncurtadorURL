using System.ComponentModel.DataAnnotations;

namespace EncurtadorURL.Config
{
    public class MongoDbSettings
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;

        [Required]
        public string DatabaseName { get; set; } = string.Empty;

        [Required]
        public string CollectionName { get; set; } = string.Empty;
    }
}
