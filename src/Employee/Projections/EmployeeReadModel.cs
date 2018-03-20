using System.ComponentModel.DataAnnotations.Schema;
using Employee.Core.DomainModel;
using Employee.Requests;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Newtonsoft.Json;

namespace Employee.Projections
{
    [Table("ReadModel-Employee")]
    public class EmployeeReadModel : IReadModel,
        IAmReadModelFor<EmployeeAggregate, EmployeeId, EmployeeCreatedEvent>,
        IAmReadModelFor<EmployeeAggregate, EmployeeId, EmployeeBankInfoAddedEvent>
    {
        //[MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }

        public string Json { get; set; }

        public EmployeeDto ToLoanApplication()
        {
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(Json);
            return employee;
        }

        //public string PersonalIdentificationNumber { get; set; }
        //public string UserName { get; set; }
        //public string BankAccountNumber { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<EmployeeAggregate, EmployeeId, EmployeeCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.GetGuid().ToString("D");

            var employee = new EmployeeDto()
            {
                Id = domainEvent.AggregateIdentity.GetGuid(),
                UserName = domainEvent.AggregateEvent.UserName,
                PersonalIdentificationNumber = domainEvent.AggregateEvent.PersonalIdentificationNumber
            };

            Json = JsonConvert.SerializeObject(employee);
        }

        public void Apply(IReadModelContext context, IDomainEvent<EmployeeAggregate, EmployeeId, EmployeeBankInfoAddedEvent> domainEvent)
        {
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(Json);
            employee.BankAccountNumber = domainEvent.AggregateEvent.BankAccountNumber;
            Json = JsonConvert.SerializeObject(employee);
        }
    }
}
