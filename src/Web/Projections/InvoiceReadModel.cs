using System.ComponentModel.DataAnnotations.Schema;
using Invoice.Core.DomainModel;
using Invoice.Response;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Newtonsoft.Json;

namespace Web.Projections
{
    [Table("ReadModel-Invoice")]
    public class InvoiceReadModel : IReadModel,
        IAmReadModelFor<InvoiceAggregate, InvoiceId, InvoiceCreatedEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }

        public string Json { get; set; }

        public InvoiceDto ToInvoiceDto()
        {
            var invoice = JsonConvert.DeserializeObject<InvoiceDto>(Json);
            return invoice;
        }

        public void Apply(IReadModelContext context, IDomainEvent<InvoiceAggregate, InvoiceId, InvoiceCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.GetGuid().ToString("D");
            var e = domainEvent.AggregateEvent;
            var invoice = new InvoiceDto()
            {
                EmployeeId = e.EmployeeId,
                CustomerId = e.CustomerId,
                EndDate = e.EndDate,
                InvoiceDescription = e.InvoiceDescription,
                InvoiceItems = e.InvoiceItems,
                Name = e.Name,
                StartDate = e.StartDate,
                Vat = e.Vat
            };

            Json = JsonConvert.SerializeObject(invoice);
        }

      
    }
}
