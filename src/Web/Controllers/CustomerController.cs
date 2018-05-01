using System;
using System.Threading.Tasks;
using Employee.Core.ApplicationServices;
using Employee.Requests;
using Employee.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet()]
        public async Task<CustomerDto[]> GetAll()
        {
            return await _customerService.GetCustomers();
        }

        [HttpGet("{customerId}")]
        public async Task<CustomerDto> GetCustomer(Guid customerId)
        {
            return await _customerService.GetCustomer(customerId);
        }

        [HttpPost("")]
        public async Task<Guid> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            return await _customerService.CreateCustomer(request);
        }
    }
}
