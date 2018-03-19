using System.Collections.Generic;
using Customer.Requests;
using EventFlow.Queries;

namespace Customer.Core.DomainServices
{
    internal class GetAllCustomersQuery : IQuery<IEnumerable<CustomerDto>>
    {
    }
}