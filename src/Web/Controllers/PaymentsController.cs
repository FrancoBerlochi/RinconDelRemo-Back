using Application.UsesCases;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly CreatePaymentUseCase _createPaymentUseCase;
        public PaymentsController(CreatePaymentUseCase createPaymentUseCase)
        {
            _createPaymentUseCase = createPaymentUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
        {
            var url = await _createPaymentUseCase.Execute(request.Amount, request.Description, request.Email);
            return Ok(new { paymentUrl = url });
        }

        public class PaymentRequestDto
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public string Email { get; set; }
        }
    }
}
