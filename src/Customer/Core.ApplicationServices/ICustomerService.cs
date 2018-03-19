using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customer.Requests;

namespace Customer.Core.ApplicationServices
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomer(CreateCustomerRequest request);
        Task<IEnumerable<CustomerDto>> GetCustomers();
    }
}
