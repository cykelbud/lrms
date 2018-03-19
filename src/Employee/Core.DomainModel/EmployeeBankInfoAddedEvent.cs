using EventFlow.Aggregates;

namespace Employee.Core.DomainModel
{
    public class EmployeeBankInfoAddedEvent : AggregateEvent<EmployeeAggregate, EmployeeId>
    {
        public string BankAccountNumber { get; }

        public EmployeeBankInfoAddedEvent(string bankAccountNumber)
        {
            BankAccountNumber = bankAccountNumber;
        }
    }
}