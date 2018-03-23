using System;

namespace Assignment.Response
{
    public class AssignmentDto
    {
        public Guid AssignmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
