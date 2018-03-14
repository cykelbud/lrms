using EventFlow.Aggregates;

namespace EmployeeService.Core.DomainModel
{
    public class EmployeeCreatedEvent : AggregateEvent<EmployeeAggregate, EmployeeId>
    {
        public string UserName { get; }
        public string PersonalIdentificationNumber { get; }

        public EmployeeCreatedEvent(string userName, string personalIdentificationNumber)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }
    }
}