using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;

namespace Invoice.Infrastructure.Subscribers
{
    public class InvoiceSubscriber : ISubscribeSynchronousToAll
    {
        private readonly IQueryProcessor _queryProcessor;

        public InvoiceSubscriber(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {

            var @event = domainEvents.Single();
            var e = @event.GetAggregateEvent();
            
            dynamic ee = @event.GetAggregateEvent();
            var un = ee.UserName;
            var pn = ee.PersonalIdentificationNumber;

            return Task.FromResult(0);
        }
    }
}
