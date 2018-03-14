using EventFlow.Aggregates;

namespace CustomerService.Core.DomainModel
{
    public class CustomerCreatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public string UserName { get; }
        public string PersonalIdentificationNumber { get; }

        public CustomerCreatedEvent(string userName, string personalIdentificationNumber)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }
    }
}