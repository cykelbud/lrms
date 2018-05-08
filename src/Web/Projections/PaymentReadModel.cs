using System.ComponentModel.DataAnnotations.Schema;
using Payment.Core.DomainModel;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Newtonsoft.Json;
using Payment.Response;

namespace Web.Projections
{
    [Table("ReadModel-Payment")]
    public class PaymentReadModel : IReadModel,
        IAmReadModelFor<PaymentAggregate, PaymentId, WaitingForPaymentEvent>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentReceivedEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }
        public string Json { get; set; }

        public PaymentDto ToPaymentDto()
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            return payment;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, WaitingForPaymentEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.Value;
            var e = domainEvent.AggregateEvent;
            var payment = new PaymentDto()
            {
               PaymentId = domainEvent.AggregateIdentity.GetGuid(),
               InvoiceId = e.InvoiceId,
            };

            Json = JsonConvert.SerializeObject(payment);
        }


        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, PaymentReceivedEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.PaymentReceivedDate = domainEvent.AggregateEvent.ReceivedDate;
            Json = JsonConvert.SerializeObject(payment);
        }
    }
}
