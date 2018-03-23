using System;
using Customer.Commands;
using EventFlow.Aggregates;

namespace Customer.Core.DomainModel
{
    public class CustomerAggregate : AggregateRoot<CustomerAggregate, CustomerId>,
        IApply<CustomerCreatedEvent>
    {
        // state
        public Guid EmployeeId { get; set; }
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }

        public CustomerAggregate(CustomerId id) : base(id)
        {
        }

        public void CreateCustomer(CreateCustomerCommand command)
        {
            Emit(new CustomerCreatedEvent(command.EmployeeId, command.UserName, command.PersonalIdentificationNumber, command.Address));
        }

        public void Apply(CustomerCreatedEvent aggregateEvent)
        {
            EmployeeId = aggregateEvent.EmployeeId;
            UserName = aggregateEvent.UserName;
            PersonalIdentificationNumber = aggregateEvent.PersonalIdentificationNumber;
        }

    }
}
