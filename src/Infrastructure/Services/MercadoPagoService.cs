using Domain.Interfaces;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace Infrastructure.Services
{
    public class MercadoPagoService : IPaymentService
    {
        public MercadoPagoService(string accessToken)
        {
            MercadoPagoConfig.AccessToken = accessToken;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string description, string payerEmail)
        {
            var client = new PreferenceClient();
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = description,
                        Quantity = 1,
                        UnitPrice = amount
                    }

                },
                Payer = new PreferencePayerRequest
                {
                    Email = payerEmail
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://localhost:7148/pagos/success",
                    Failure = "https://localhost:7148/pagos/failure",
                    Pending = "https://localhost:7148/pagos/pending"
                },
                AutoReturn = "approved"
            };
            Preference preference = await client.CreateAsync(request);
            return preference.InitPoint; // esta es la URL para redirigir al usuario
        }
    }
}
