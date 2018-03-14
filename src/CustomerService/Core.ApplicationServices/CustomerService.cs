using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerService.Commands;
using CustomerService.Core.DomainModel;
using CustomerService.Requests;
using EventFlow;

namespace CustomerService.Core.ApplicationServices
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICommandBus _commandBus;

        public CustomerService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task<Guid> CreateCustomer(CreateCustomerRequest request)
        {
            var customerId = CustomerId.New;
            await _commandBus.PublishAsync(new CreateCustomerCommand(customerId, request.UserName, request.PersonalIdentificationNumber), CancellationToken.None);
            return customerId.GetGuid();
        }
    }
}