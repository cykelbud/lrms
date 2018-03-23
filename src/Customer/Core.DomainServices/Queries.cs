using System;
using Customer.Requests;
using EventFlow.Queries;

namespace Customer.Core.DomainServices
{
    public class GetAllCustomersQuery : IQuery<CustomerDto[]>
    {
    }

    public class GetCustomerQuery : IQuery<CustomerDto>
    {
        public Guid Id { get; }

        public GetCustomerQuery(Guid id)
        {
            Id = id;
        }
    }
}