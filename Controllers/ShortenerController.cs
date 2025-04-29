using EncurtadorURL.DTOs;
using EncurtadorURL.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorURL.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar URLs encurtadas.
    /// </summary>
    public class ShortenerController(ILogger<ShortenerController> logger, ShortUrlService shortUrlService, PaymentService paymentService) : Controller
    {
        private readonly ILogger<ShortenerController> _logger = logger;
        private readonly ShortUrlService _shortUrlService = shortUrlService;
        private readonly PaymentService _paymentService = paymentService;

        /// <summary>
        /// Exibe a página inicial do encurtador de URLs.
        /// </summary>
        /// <returns>Uma view representando a página inicial.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Cria uma nova URL encurtada com base nos dados fornecidos.
        /// </summary>
        /// <param name="createRequest">Objeto contendo os dados necessários para criar a URL encurtada.</param>
        /// <returns>
        /// Retorna a view de sucesso com os detalhes da URL encurtada, 
        /// ou a página inicial caso os dados sejam inválidos.
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
                    return RedirectToAction("Payment", "Error"); // Exibe um erro se o pagamento não for confirmado
                }
            }

            var shortUrlResponse = await _shortUrlService.CreateShortUrlAsync(createRequest);

            return View("Success", shortUrlResponse);
        }

        /// <summary>
        /// Redireciona o usuário para a URL original com base no código curto fornecido.
        /// </summary>
        /// <param name="shortCode">O código curto da URL encurtada.</param>
        /// <returns>
        /// Redireciona para a URL original se encontrada, 
        /// ou para a página de erro 404 caso o código não seja válido.
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
