using EventFlow.Aggregates;

namespace Employee.Core.DomainModel
{
    public class EmployeeAggregate : AggregateRoot<EmployeeAggregate, EmployeeId>,
        IApply<EmployeeCreatedEvent>,
        IApply<EmployeeBankInfoAddedEvent>
    {
        // state
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string BankAccountNumber { get; set; }


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

        public void AddBankInfo(AddBankInfoCommand command)
        {
            Emit(new EmployeeBankInfoAddedEvent(command.BankAccountNumber));
        }

        public void Apply(EmployeeBankInfoAddedEvent aggregateEvent)
        {
            BankAccountNumber = aggregateEvent.BankAccountNumber;
        }

    }
}
