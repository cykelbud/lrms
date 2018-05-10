using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class DebtCollectionEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public DateTime DebtCollectionDate { get; }

        public DebtCollectionEvent(Guid invoiceId, DateTime debtCollectionDate)
        {
            InvoiceId = invoiceId;
            DebtCollectionDate = debtCollectionDate;
        }
    }
}