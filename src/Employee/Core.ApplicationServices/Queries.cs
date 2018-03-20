using System;
using Employee.Requests;
using EventFlow.Queries;

namespace Employee.Core.ApplicationServices
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
}