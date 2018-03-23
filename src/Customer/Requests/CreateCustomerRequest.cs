using System;

namespace Customer.Requests
{
    public class CreateCustomerRequest
    {
        public Guid EmployeeId { get; set; }
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string Address { get; set; }
    }
}
