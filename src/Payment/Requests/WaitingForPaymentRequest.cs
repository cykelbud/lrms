using System;

namespace Payment.Requests
{
    public class WaitingForPaymentRequest
    {
        public Guid InvoiceId { get; set; }
    }
}