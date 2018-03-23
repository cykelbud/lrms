using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class WaitingForPaymentEvent : IAggregateEvent<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public WaitingForPaymentEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}