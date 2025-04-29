using EncurtadorURL.DTOs;
using EncurtadorURL.Models;
using EncurtadorURL.Models.Enums;
using EncurtadorURL.Repositories;

namespace EncurtadorURL.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar a criação e recuperação de URLs encurtadas.
    /// </summary>
    public class ShortUrlService(ShortUrlRepository shortUrlRepository, IConfiguration configuration)
    {
        private readonly ShortUrlRepository _shortUrlRepository = shortUrlRepository;
        private readonly string _baseDomainUrl = configuration.GetValue<string>("DomainUrl");

        /// <summary>
        /// Cria uma nova URL encurtada com base nos dados fornecidos.
        /// </summary>
        /// <param name="createRequest">Objeto contendo os dados necessários para criar a URL encurtada.</param>
        /// <returns>Um objeto <see cref="ShortUrlResponse"/> contendo a URL encurtada e a data de expiração.</returns>
        /// <exception cref="ArgumentNullException">Lançada se o <paramref name="createRequest"/> for nulo.</exception>
        public async Task<ShortUrlResponse> CreateShortUrlAsync(CreateShortUrlRequest createRequest)
        {
            if (createRequest == null)
                throw new ArgumentNullException(nameof(createRequest), "O objeto de requisição não pode ser nulo.");

            string shortCode;

            // Gera um código curto único.
            do
            {
                shortCode = GenerateShortCode();
            }
            while (await _shortUrlRepository.ExistsShortCodeAsync(shortCode));

            var shortUrl = new ShortUrl
            {
                ShortCode = shortCode,
                LongUrl = createRequest.LongUrl,
                ExpiresAt = CalculateExpirationDate(createRequest.ExpirationAt),
                ExpirationAt = createRequest.ExpirationAt
            };

            await _shortUrlRepository.CreateAsync(shortUrl);

            return new ShortUrlResponse
            {
                ShortUrl = _baseDomainUrl.EndsWith("/")
                ? $"{_baseDomainUrl}{shortCode}"
                : $"{_baseDomainUrl}/{shortCode}",
                ExpirationAt = shortUrl.ExpiresAt
            };
        }

        /// <summary>
        /// Recupera a URL original com base no código curto fornecido.
        /// </summary>
        /// <param name="shortCode">O código curto da URL encurtada.</param>
        /// <returns>A URL original se encontrada; caso contrário, <c>null</c>.</returns>
        public async Task<string?> GetLongUrlAsync(string shortCode)
        {
            if (string.IsNullOrWhiteSpace(shortCode))
                throw new ArgumentException("O código curto não pode ser nulo ou vazio.", nameof(shortCode));

            var shortUrl = await _shortUrlRepository.GetByShortCodeAsync(shortCode);

            // Verifica se a URL existe e se não está expirada.
            if (shortUrl == null || (shortUrl.ExpiresAt.HasValue && shortUrl.ExpiresAt.Value < DateTime.UtcNow))
                return null;

            return shortUrl.LongUrl;
        }

        /// <summary>
        /// Gera um código curto aleatório.
        /// </summary>
        /// <returns>Um código curto de 6 caracteres.</returns>
        private static string GenerateShortCode()
        {
            return Guid.NewGuid().ToString("N")[..6]; // Gera 6 caracteres aleatórios.
        }

        /// <summary>
        /// Calcula a data de expiração com base no tipo de expiração fornecido.
        /// </summary>
        /// <param name="expirationType">O tipo de expiração.</param>
        /// <returns>A data de expiração calculada ou <c>null</c> se não houver expiração.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Lançada se o tipo de expiração for inválido.</exception>
        private static DateTime? CalculateExpirationDate(ExpirationAtEnum expirationType)
        {
            return expirationType switch
            {
                ExpirationAtEnum.OneHour => DateTime.UtcNow.AddHours(1),
                ExpirationAtEnum.OneDay => DateTime.UtcNow.AddDays(1),
                ExpirationAtEnum.ThreeDays => DateTime.UtcNow.AddDays(3),
                ExpirationAtEnum.SevenDays => DateTime.UtcNow.AddDays(7),
                ExpirationAtEnum.OneMonth => DateTime.UtcNow.AddMonths(1),
                ExpirationAtEnum.Never => null,
                _ => throw new ArgumentOutOfRangeException(nameof(expirationType), "Tipo de expiração inválido.")
            };
        }
    }
}
