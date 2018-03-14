using System;

namespace AssignmentService.Requests
{
    public class CreateAssignmentRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
