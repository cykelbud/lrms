using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Invoice.Core.DomainModel;
using Invoice.Response;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Newtonsoft.Json;
using Payout.Response;

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

        public PayoutInvoiceDto ToPayoutInvoiceDto()
        {
            var invoice = JsonConvert.DeserializeObject<InvoiceDto>(Json);
            return new PayoutInvoiceDto()
            {
                Amount = invoice.InvoiceItems.Sum(i => i.Price),
                EmployeeId = invoice.EmployeeId,
                InvoiceId = invoice.InvoiceId,
            };
        }

        public void Apply(IReadModelContext context, IDomainEvent<InvoiceAggregate, InvoiceId, InvoiceCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.Value;
            var e = domainEvent.AggregateEvent;
            var invoice = new InvoiceDto()
            {
                InvoiceId = domainEvent.AggregateIdentity.GetGuid(),
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
