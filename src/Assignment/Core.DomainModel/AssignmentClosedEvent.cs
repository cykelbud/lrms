using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Assignment.Core.DomainModel
{
    [EventVersion("AssignmentClosedEvent", 1)]
    public class AssignmentClosedEvent : IAggregateEvent<AssignmentAggregate, AssignmentId>
    {
    }
}