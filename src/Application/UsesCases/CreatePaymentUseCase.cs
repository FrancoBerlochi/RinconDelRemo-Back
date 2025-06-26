using Domain.Interfaces;

namespace Application.UsesCases
{
    public class CreatePaymentUseCase
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentUseCase(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<string> Execute(decimal amount, string description, string payerEmail)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be empty.", nameof(description));
            }
            if (string.IsNullOrWhiteSpace(payerEmail))
            {
                throw new ArgumentException("Payer email cannot be empty.", nameof(payerEmail));
            }
            return await _paymentService.CreatePaymentAsync(amount, description, payerEmail);
        }
    }
}
