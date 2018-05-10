using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<PaymentDto>> GetAll()
        {
            return await _paymentService.GetAll();
        }

        [HttpGet("{invoiceId}")]
        public async Task<PaymentDto> GetPayment(Guid invoiceId)
        {
            return await _paymentService.GetPayment(invoiceId);
        }


        [HttpPost("")]
        public async Task SimulateReceivePayment([FromBody] ReceivePaymentRequest request)
        {
            await _paymentService.SimulateReceivePayment(request);
        }

        [HttpPost("")]
        public async Task SimulatePaymentDue([FromBody] PaymentDueRequest request)
        {
            await _paymentService.SimulatePaymentDue(request);
        }

        [HttpPost("{invoiceId}")]
        public async Task SimulatePaymentDebtCollection(Guid invoiceId)
        {
            await _paymentService.SimulateDebtCollection(invoiceId);
        }

        [HttpPost("{invoiceId}")]
        public async Task SimulatePaymentPaymentInjuction(Guid invoiceId)
        {
            await _paymentService.SimulatePaymentInjunction(invoiceId);
        }

        [HttpPost("{invoiceId}")]
        public async Task SimulatePaymentDistraint(Guid invoiceId)
        {
            await _paymentService.SimulateDistraint(invoiceId);
        }
    }
}
