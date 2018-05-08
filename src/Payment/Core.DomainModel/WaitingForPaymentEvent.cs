using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Payment.Core.DomainModel
{
    [EventVersion("WaitingForPaymentEvent", 1)]
    public class WaitingForPaymentEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public WaitingForPaymentEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}