namespace EncurtadorURL.Services
{
    public class PaymentService
    {
        public PaymentService()
        {

        }

        public async Task<bool> ValidatePaymentAsync()
        {
            // Simulação de validação de pagamento
            await Task.Delay(2000); // Simula uma chamada assíncrona
            return true; // Retorna verdadeiro para simular um pagamento confirmado
        }
    }
}
