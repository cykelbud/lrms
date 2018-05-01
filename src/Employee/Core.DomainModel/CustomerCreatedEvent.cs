using System;
using EventFlow.Aggregates;

namespace Employee.Core.DomainModel
{
    public class CustomerCreatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public Guid EmployeeId { get; }
        public string UserName { get; }
        public string PersonalIdentificationNumber { get; }
        public string Address { get; }

        public CustomerCreatedEvent(Guid employeeId, string userName, string personalIdentificationNumber, string address)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
            Address = address;
            EmployeeId = employeeId;
        }
    }
}