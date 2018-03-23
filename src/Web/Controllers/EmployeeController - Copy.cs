using System.Threading.Tasks;
using Employee.Core.ApplicationServices;
using Employee.Requests;
using Employee.Response;
using Microsoft.AspNetCore.Mvc;
using Payment.Core.ApplicationServices;
using Payment.Requests;
using Payment.Response;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet()]
        public async Task<PaymentDto[]> GetAllE()
        {
            return await _paymentService.GetAll();
        }


        [HttpPost("")]
        public async Task SimulateReceivePayment([FromBody] ReceivePaymentRequest request)
        {
            await _paymentService.ReceivePayment(request);
        }

        [HttpPost("")]
        public async Task SimulatePaymentDue([FromBody] PaymentDueRequest request)
        {
            await _paymentService.PaymentDue(request);
        }
    }
}
