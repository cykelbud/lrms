using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;

namespace Employee.Infrastructure.Subscribers
{
    public class EmployeeEventSubscriber : ISubscribeSynchronousToAll
    {
        private readonly IQueryProcessor _queryProcessor;

        public EmployeeEventSubscriber(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {

            var domainEvent = domainEvents.Single();

            if (domainEvent.EventType.FullName == "Customer.Core.DomainModel.CustomerCreatedEvent")
            {
                // store event info
                dynamic aggregateEvent = domainEvent.GetAggregateEvent();
            }


            return Task.FromResult(0);
        }
    }
}
