using System;
using CustomerService.Requests;
using EventFlow;

namespace CustomerService.Core.ApplicationServices
{
    internal interface ICustomerService
    {
        Guid CreateCustomer(CreateCustomerRequest request);
    }

    internal class CustomerService : ICustomerService
    {
        private readonly ICommandBus _commandBus;

        public CustomerService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Guid CreateCustomer(CreateCustomerRequest request)
        {
            _commandBus.PublishAsync()
        }
    }
}
