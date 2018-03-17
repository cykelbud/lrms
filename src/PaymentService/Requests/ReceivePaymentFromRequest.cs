using System;

namespace PaymentService.Requests
{
    public class ReceivePaymentFromRequest
    {
        public Guid InvoiceId { get; set; }
    }
}
