using System;

namespace Customer.Requests
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
    }
}