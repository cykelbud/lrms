using System;
using System.Threading.Tasks;
using Invoice.Core.ApplicationServices;
using Invoice.Requests;
using Invoice.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("")]
        public async Task<Guid> CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            return await _invoiceService.CreateInvoice(request);
        }

        [HttpPost("send/{invoiceId}")]
        public async Task SendInvoice([FromBody] Guid invoiceId)
        {
            await _invoiceService.SendInvoice(new SendInvoiceRequest()
            {
                InvoiceId = invoiceId
            });
        }

        [HttpGet("{invoiceId}")]
        public async Task<InvoiceDto> GetInvoice(Guid invoiceId)
        {
            return await _invoiceService.GetInvoice(invoiceId);
        }
    }
}
