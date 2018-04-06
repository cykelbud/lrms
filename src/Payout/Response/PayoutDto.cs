using System;

namespace Payout.Response
{
    public class PayoutDto
    {
        public Guid PayoutId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}