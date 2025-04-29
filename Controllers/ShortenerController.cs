using EncurtadorURL.DTOs;
using EncurtadorURL.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorURL.Controllers
{
    /// <summary>
    /// Controlador respons�vel por gerenciar URLs encurtadas.
    /// </summary>
    public class ShortenerController(ILogger<ShortenerController> logger, ShortUrlService shortUrlService, PaymentService paymentService) : Controller
    {
        private readonly ILogger<ShortenerController> _logger = logger;
        private readonly ShortUrlService _shortUrlService = shortUrlService;
        private readonly PaymentService _paymentService = paymentService;

        /// <summary>
        /// Exibe a p�gina inicial do encurtador de URLs.
        /// </summary>
        /// <returns>Uma view representando a p�gina inicial.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Cria uma nova URL encurtada com base nos dados fornecidos.
        /// </summary>
        /// <param name="createRequest">Objeto contendo os dados necess�rios para criar a URL encurtada.</param>
        /// <returns>
        /// Retorna a view de sucesso com os detalhes da URL encurtada, 
        /// ou a p�gina inicial caso os dados sejam inv�lidos.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateShortUrlRequest createRequest)
        {
            if (!ModelState.IsValid)
                return View("Index");

            if (createRequest.ExpirationAt == Models.Enums.ExpirationAtEnum.SevenDays
                || createRequest.ExpirationAt == Models.Enums.ExpirationAtEnum.OneMonth
                || createRequest.ExpirationAt == Models.Enums.ExpirationAtEnum.Never)
            {
                var isPaymentConfirmed = await _paymentService.ValidatePaymentAsync();

                if (!isPaymentConfirmed)
                {
                    return RedirectToAction("Payment", "Error"); // Exibe um erro se o pagamento n�o for confirmado
                }
            }

            var shortUrlResponse = await _shortUrlService.CreateShortUrlAsync(createRequest);

            return View("Success", shortUrlResponse);
        }

        /// <summary>
        /// Redireciona o usu�rio para a URL original com base no c�digo curto fornecido.
        /// </summary>
        /// <param name="shortCode">O c�digo curto da URL encurtada.</param>
        /// <returns>
        /// Redireciona para a URL original se encontrada, 
        /// ou para a p�gina de erro 404 caso o c�digo n�o seja v�lido.
        /// </returns>
        [HttpGet("/{shortCode}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortCode)
        {
            var originalUrl = await _shortUrlService.GetLongUrlAsync(shortCode);

            if (originalUrl == null)
                return RedirectToAction("NotFound", "Error");

            return Redirect(originalUrl);
        }
    }
}
