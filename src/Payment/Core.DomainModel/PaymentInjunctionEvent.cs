using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentInjunctionEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public DateTime PaymentInjunctionDate { get; }

        public PaymentInjunctionEvent(Guid invoiceId, DateTime paymentInjunctionDate)
        {
            InvoiceId = invoiceId;
            PaymentInjunctionDate = paymentInjunctionDate;
        }
    }
}