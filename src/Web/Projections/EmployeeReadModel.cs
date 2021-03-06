﻿using System.ComponentModel.DataAnnotations.Schema;
using Employee.Core.DomainModel;
using Employee.Response;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Invoice.Response;
using Newtonsoft.Json;
using Payout.Response;

namespace Web.Projections
{
    [Table("ReadModel-Employee")]
    public class EmployeeReadModel : IReadModel,
        IAmReadModelFor<EmployeeAggregate, EmployeeId, EmployeeCreatedEvent>,
        IAmReadModelFor<EmployeeAggregate, EmployeeId, EmployeeBankInfoAddedEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }

        public string Json { get; set; }

        public EmployeeDto ToEmployeeDto()
        {
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(Json);
            return employee;
        }

        public InvoiceEmployeeDto ToInvoiceEmployeeDto()
        {
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(Json);
            var invoiceCustomer = new InvoiceEmployeeDto()
            {
                EmployeeId = employee.Id,
                Name = employee.UserName
            };
            return invoiceCustomer;
        }

        public PayoutEmployeeDto ToPayoutEmployeeDto()
        {
            var employee = JsonConvert.DeserializeObject<EmployeeDto>(Json);
            var payoutEmployee = new PayoutEmployeeDto()
            {
                EmployeeId = employee.Id,
                BankAccountNumber = employee.BankAccountNumber
            };
            return payoutEmployee;
        }



        public void Apply(IReadModelContext context, IDomainEvent<EmployeeAggregate, EmployeeId, EmployeeCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.Value;

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
