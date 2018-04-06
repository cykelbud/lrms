using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentClosedEvent : IAggregateEvent<AssignmentAggregate, AssignmentId>
    {
    }
}