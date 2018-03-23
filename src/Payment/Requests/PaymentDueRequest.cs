using System;

namespace Payment.Requests
{
    public class PaymentDueRequest
    {
        public Guid InvoiceId { get; set; }
    }
}