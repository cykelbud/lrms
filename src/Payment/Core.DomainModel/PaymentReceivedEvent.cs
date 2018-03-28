using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentReceivedEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public DateTime ReceivedDate { get; }

        public PaymentReceivedEvent(Guid invoiceId, DateTime receivedDate)
        {
            InvoiceId = invoiceId;
            ReceivedDate = receivedDate;
        }
    }
}