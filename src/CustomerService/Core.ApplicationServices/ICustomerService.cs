using System;
using System.Threading.Tasks;
using CustomerService.Requests;

namespace CustomerService.Core.ApplicationServices
{
    internal interface ICustomerService
    {
        Task<Guid> CreateCustomer(CreateCustomerRequest request);
    }
}
