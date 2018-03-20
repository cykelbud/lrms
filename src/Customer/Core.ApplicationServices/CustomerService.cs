﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Customer.Commands;
using Customer.Core.DomainModel;
using Customer.Core.DomainServices;
using Customer.Requests;
using EventFlow;
using EventFlow.Queries;

namespace Customer.Core.ApplicationServices
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
                new CreateCustomerCommand(customerId, request.UserName, request.PersonalIdentificationNumber),
                CancellationToken.None);
            return customerId.GetGuid();
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = await _queryProcessor.ProcessAsync(new GetAllCustomersQuery(), CancellationToken.None).ConfigureAwait(false);
            return customers;
        }
    }
}