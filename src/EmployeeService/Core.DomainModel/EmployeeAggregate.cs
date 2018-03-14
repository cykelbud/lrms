using EventFlow.Aggregates;

namespace EmployeeService.Core.DomainModel
{
    public class EmployeeAggregate : AggregateRoot<EmployeeAggregate, EmployeeId>,
        IApply<EmployeeCreatedEvent>
    {
        // state
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }

        public EmployeeAggregate(EmployeeId id) : base(id)
        {
        }

        public void CreateCustomer(CreateEmployeeCommand command)
        {
            Emit(new EmployeeCreatedEvent(command.UserName, command.PersonalIdentificationNumber));
        }

        public void Apply(EmployeeCreatedEvent aggregateEvent)
        {
            UserName = aggregateEvent.UserName;
            PersonalIdentificationNumber = aggregateEvent.PersonalIdentificationNumber;
        }

    }
}
