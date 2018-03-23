using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentAggregate : AggregateRoot<AssignmentAggregate, AssignmentId>,
        IApply<AssignmentCreatedEvent>
    {
        // state
        public Guid EmployeeId { get; set; }


        public AssignmentAggregate(AssignmentId id) : base(id)
        {
        }
        
        public void CreateAssignment(CreateAssignmentCommand command)
        {
            Emit(new AssignmentCreatedEvent(command.EmployeeId));
        }

        public void Apply(AssignmentCreatedEvent aggregateEvent)
        {
            EmployeeId = aggregateEvent.EmployeeId;
        }
    }
}
