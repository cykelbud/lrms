using System;

namespace Payment.Response
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
    }
}