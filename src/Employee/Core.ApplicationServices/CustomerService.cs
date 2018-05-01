using System;
using System.Threading;
using System.Threading.Tasks;
using Employee.Core.DomainModel;
using Employee.Core.DomainServices;
using Employee.Requests;
using Employee.Response;
using EventFlow;
using EventFlow.Queries;

namespace Employee.Core.ApplicationServices
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public CustomerService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<Guid> CreateCustomer(CreateCustomerRequest request)
        {
            var customerId = CustomerId.New;
            await _commandBus.PublishAsync(
                new CreateCustomerCommand(customerId, request.EmployeeId, request.UserName, request.PersonalIdentificationNumber, request.Address),
                CancellationToken.None).ConfigureAwait(false);
            return customerId.GetGuid();
        }

        public async Task<CustomerDto[]> GetCustomers()
        {
            var customers = await _queryProcessor.ProcessAsync(new GetAllCustomersQuery(), CancellationToken.None).ConfigureAwait(false);
            return customers;
        }

        public async Task<CustomerDto> GetCustomer(Guid customerId)
        {
            var customer = await _queryProcessor.ProcessAsync(new GetCustomerQuery(customerId), CancellationToken.None).ConfigureAwait(false);
            return customer;
        }
    }
}