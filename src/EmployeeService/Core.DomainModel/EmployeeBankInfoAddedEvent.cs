using EventFlow.Aggregates;

namespace EmployeeService.Core.DomainModel
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