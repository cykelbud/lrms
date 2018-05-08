using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Assignment.Core.DomainModel
{
    [EventVersion("AssignmentInWaitingForPaymentStateEvent", 1)]
    public class AssignmentInWaitingForPaymentStateEvent : IAggregateEvent<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public AssignmentInWaitingForPaymentStateEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}