
namespace Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(decimal amount, string description, string payerEmail);
    }
}
