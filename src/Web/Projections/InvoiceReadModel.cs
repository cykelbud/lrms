using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Assignment.Response;
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
        IAmReadModelFor<InvoiceAggregate, InvoiceId, InvoiceCreatedEvent>,
        IAmReadModelFor<InvoiceAggregate, InvoiceId, InvoiceSentEvent>,
        IAmReadModelFor<InvoiceAggregate, InvoiceId, InvoiceReminderSentEvent>
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
                PayInAdvance = invoice.PayInAdvance,
            };
        }

        public AssignmentInvoiceDto ToAssignmentInvoiceDto()
        {
            var invoice = JsonConvert.DeserializeObject<InvoiceDto>(Json);
            return new AssignmentInvoiceDto()
            {
                InvoiceId = invoice.InvoiceId,
                PayInAdvance = invoice.PayInAdvance,
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
                Vat = e.Vat,
                PayInAdvance = e.PayInAdvance,
                HasTaxReduction = e.HasTaxReduction
            };

            Json = JsonConvert.SerializeObject(invoice);
        }


        public void Apply(IReadModelContext context, IDomainEvent<InvoiceAggregate, InvoiceId, InvoiceSentEvent> domainEvent)
        {
            var dto = JsonConvert.DeserializeObject<InvoiceDto>(Json);
            dto.InvoiceSentDate = domainEvent.AggregateEvent.InvoiceSentDate;
            Json = JsonConvert.SerializeObject(dto);
        }

        public void Apply(IReadModelContext context, IDomainEvent<InvoiceAggregate, InvoiceId, InvoiceReminderSentEvent> domainEvent)
        {
            var dto = JsonConvert.DeserializeObject<InvoiceDto>(Json);
            dto.ReminderSentDate = domainEvent.AggregateEvent.ReminderSentDate;
            Json = JsonConvert.SerializeObject(dto);
        }
    }
}
