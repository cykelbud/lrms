using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentReceivedEvent : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public DateTime ReceivedDate { get; }
        public decimal Amount { get; }

        public PaymentReceivedEvent(Guid invoiceId, DateTime receivedDate, decimal amount)
        {
            InvoiceId = invoiceId;
            ReceivedDate = receivedDate;
            Amount = amount;
        }
    }
}