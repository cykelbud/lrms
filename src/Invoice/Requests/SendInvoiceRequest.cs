using System;

namespace Invoice.Requests
{
    public class SendInvoiceRequest
    {
        public Guid InvoiceId { get; set; }
    }
}