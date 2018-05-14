using System;

namespace Assignment.Response
{
    public class AssignmentDto
    {
        public Guid AssignmentId { get; set; }
        public Guid InvoiceId { get; set; }
        public Status CurrentStatus { get; set; }
    }

    public class AssignmentInvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public bool PayInAdvance { get; set; }
    }

    public enum Status
    {
        Created,
        WaitingForPaymentFromCustomer,
        WaitingForPaymentFromSkattverket,
        Closed,
    }
}
