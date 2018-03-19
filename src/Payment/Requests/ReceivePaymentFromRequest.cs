using System;

namespace Payment.Requests
{
    public class ReceivePaymentFromRequest
    {
        public Guid InvoiceId { get; set; }
    }
}
