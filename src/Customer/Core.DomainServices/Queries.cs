using System;
using Customer.Requests;
using EventFlow.Queries;

namespace Customer.Core.DomainServices
{
    internal class GetAllCustomersQuery : IQuery<CustomerDto[]>
    {
    }

    internal class GetCustomerQuery : IQuery<CustomerDto>
    {
        public Guid Id { get; }

        public GetCustomerQuery(Guid id)
        {
            Id = id;
        }
    }
}