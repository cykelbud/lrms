using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class DistraintEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public DateTime DistraintDate { get; }

        public DistraintEvent(Guid invoiceId, DateTime distraintDate)
        {
            InvoiceId = invoiceId;
            DistraintDate = distraintDate;
        }
    }
}