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
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentReceivedEvent>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentDueEvent>,
        IAmReadModelFor<PaymentAggregate, PaymentId, DebtCollectionEvent>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentInjunctionEvent>,
        IAmReadModelFor<PaymentAggregate, PaymentId, DistraintEvent>
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
               PaymentReceivedDate = null,
               CurrentState = PaymentState.WaitingForPayment
            };

            Json = JsonConvert.SerializeObject(payment);
        }


        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, PaymentReceivedEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.PaymentReceivedDate = domainEvent.AggregateEvent.ReceivedDate;
            payment.CurrentState = PaymentState.PaymentReceived;
            payment.Amount = domainEvent.AggregateEvent.Amount;
            Json = JsonConvert.SerializeObject(payment);
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, PaymentDueEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.CurrentState = PaymentState.PaymentDue;
            Json = JsonConvert.SerializeObject(payment);
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, DebtCollectionEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.CurrentState = PaymentState.DebtCollection;
            Json = JsonConvert.SerializeObject(payment);
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, PaymentInjunctionEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.CurrentState = PaymentState.PaymentInjuction;
            Json = JsonConvert.SerializeObject(payment);
        }

        public void Apply(IReadModelContext context, IDomainEvent<PaymentAggregate, PaymentId, DistraintEvent> domainEvent)
        {
            var payment = JsonConvert.DeserializeObject<PaymentDto>(Json);
            payment.CurrentState = PaymentState.Distraint;
            Json = JsonConvert.SerializeObject(payment);
        }
    }
}
