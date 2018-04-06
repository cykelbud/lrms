using System;

namespace Payout.Response
{
    public class PayoutInvoiceDto
    {
        public Guid EmployeeId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
    }
}