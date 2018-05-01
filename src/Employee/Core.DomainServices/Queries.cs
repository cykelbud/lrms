using System;
using Employee.Response;
using EventFlow.Queries;

namespace Employee.Core.DomainServices
{

    public class GetAllEmployeesQuery : IQuery<EmployeeDto[]>
    {
    }

    public class GetEmployeeQuery : IQuery<EmployeeDto>
    {
        public Guid Id { get; }

        public GetEmployeeQuery(Guid id)
        {
            Id = id;
        }
    }

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