using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentCreatedEvent : AggregateEvent<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public AssignmentCreatedEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}