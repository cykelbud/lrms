using CustomerService.Commands;
using EventFlow.Aggregates;

namespace CustomerService.Core.DomainModel
{
    public class CustomerAggregate : AggregateRoot<CustomerAggregate, CustomerId>,
        IApply<CustomerCreatedEvent>
    {
        // state
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }

        public CustomerAggregate(CustomerId id) : base(id)
        {
        }

        public void CreateCustomer(CreateCustomerCommand command)
        {
            Emit(new CustomerCreatedEvent(command.UserName, command.PersonalIdentificationNumber));
        }

        public void Apply(CustomerCreatedEvent aggregateEvent)
        {
            UserName = aggregateEvent.UserName;
            PersonalIdentificationNumber = aggregateEvent.PersonalIdentificationNumber;
        }

    }
}
