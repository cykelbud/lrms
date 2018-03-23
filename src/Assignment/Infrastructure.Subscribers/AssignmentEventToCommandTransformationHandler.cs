using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Core.DomainModel;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;

namespace Assignment.Infrastructure.Subscribers
{
    public class AssignmentEventToCommandTransformationHandler : ISubscribeSynchronousToAll
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IQueryProcessor _queryProcessor;

        public AssignmentEventToCommandTransformationHandler(IAssignmentService assignmentService, IQueryProcessor queryProcessor)
        {
            _assignmentService = assignmentService;
            _queryProcessor = queryProcessor;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var domainEvent = domainEvents.Single();

            // state-machine

            if (domainEvent.EventType.FullName == "Invoice.Core.DomainModel.InvoiceCreatedEvent")
            {
                // Initiate process for this invoice
                dynamic invoiceCreated = domainEvent.GetAggregateEvent();
                Guid employeeId = invoiceCreated.EmployeeId;
                _assignmentService.CreateAssignment(new CreateAssignmentCommand(AssignmentId.New, employeeId));
            }


            return Task.CompletedTask;
        }
    }
}
