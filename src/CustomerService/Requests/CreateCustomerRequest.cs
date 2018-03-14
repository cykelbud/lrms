using System;

namespace CustomerService.Requests
{
    public class CreateCustomerRequest
    {
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
    }
}
