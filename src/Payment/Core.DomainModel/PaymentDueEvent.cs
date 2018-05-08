using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentDueEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public PaymentDueEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}