using System;
using System.Threading.Tasks;
using Employee.Requests;
using Employee.Response;

namespace Employee.Core.ApplicationServices
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomer(CreateCustomerRequest request);
        Task<CustomerDto[]> GetCustomers();
        Task<CustomerDto> GetCustomer(Guid customerId);
    }
}
