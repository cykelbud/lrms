using System.ComponentModel.DataAnnotations.Schema;
using Customer.Requests;
using Customer.Core.DomainModel;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Invoice.Response;
using Newtonsoft.Json;

namespace Web.Projections
{
    [Table("ReadModel-Customer")]
    public class CustomerReadModel : IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, CustomerCreatedEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }
        public string Json { get; set; }

        public CustomerDto ToCustomerDto()
        {
            var customer = JsonConvert.DeserializeObject<CustomerDto>(Json);
            return customer;
        }

        public InvoiceCustomerDto ToInvoiceCustomerDto()
        {
            var customer = JsonConvert.DeserializeObject<CustomerDto>(Json);
            var invoiceCustomer = new InvoiceCustomerDto()
            {
                CustomerId = customer.Id,
                Name = customer.UserName,
                Address = customer.Address
            };
            return invoiceCustomer;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, CustomerCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.GetGuid().ToString("D");

            var customer = new CustomerDto()
            {
                Id = domainEvent.AggregateIdentity.GetGuid(),
                UserName = domainEvent.AggregateEvent.UserName,
                PersonalIdentificationNumber = domainEvent.AggregateEvent.PersonalIdentificationNumber
            };

            Json = JsonConvert.SerializeObject(customer);
        }

    }
}
