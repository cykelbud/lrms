using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Assignment.Core.DomainModel
{
    [EventVersion("AssignmentCreatedEvent", 1)]
    public class AssignmentCreatedEvent : AggregateEvent<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public AssignmentCreatedEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}