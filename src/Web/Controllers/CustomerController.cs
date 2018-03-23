using System;
using System.Threading.Tasks;
using Customer.Core.ApplicationServices;
using Customer.Requests;
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
        public async Task<CustomerDto> GetEmployee(Guid customerId)
        {
            return await _customerService.GetCustomer(customerId);
        }

        [HttpPost("")]
        public async Task CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            await _customerService.CreateCustomer(request);
        }
    }
}
