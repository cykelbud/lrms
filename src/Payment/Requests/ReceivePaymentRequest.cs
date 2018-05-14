using System;

namespace Payment.Requests
{
    public class ReceivePaymentRequest
    {
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
    }
}
