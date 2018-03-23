using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class WaitingForPaymentEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public WaitingForPaymentEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}