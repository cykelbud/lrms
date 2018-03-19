using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Invoice.Core.ApplicationServices;
using Invoice.Requests;

namespace Invoice
{
    public class InvoiceController // : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

       // [HttpPost("")]
        public async Task CreateInvoice(
        //    [FromBody]
        CreateInvoiceRequest request)
        {
            await _invoiceService.CreateInvoice(request);
        }

        //[HttpPost("send/{invoiceId}")]
        public async Task SendInvoice(
            //[FromBody] 
            Guid invoiceId)
        {
            await _invoiceService.SendInvoice(new SendInvoiceRequest()
            {
                InvoiceId = invoiceId
            });
        }
    }
}
