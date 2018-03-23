using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentCreatedEvent : AggregateEvent<AssignmentAggregate, AssignmentId>
    {
        public Guid EmployeeId { get; }

        public AssignmentCreatedEvent(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}