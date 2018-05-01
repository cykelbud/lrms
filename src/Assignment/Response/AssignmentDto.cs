using System;

namespace Assignment.Response
{
    public class AssignmentDto
    {
        public Guid AssignmentId { get; set; }
        public Guid InvoiceId { get; set; }
        public Status CurrentStatus { get; set; }
    }

    public enum Status
    {
        Created,
        WaitingForPaymentFromCustomer,
        Closed
    }
}
