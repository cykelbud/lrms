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
using Payment.Core.ApplicationServices;
using Payment.Requests;

namespace Assignment.Infrastructure.Subscribers
{
    public class AssignmentEventToCommandTransformationHandler : ISubscribeSynchronousToAll
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IPaymentService _paymentService;

        public AssignmentEventToCommandTransformationHandler(IAssignmentService assignmentService, 
            IQueryProcessor queryProcessor,
            IPaymentService paymentService)
        {
            _assignmentService = assignmentService;
            _queryProcessor = queryProcessor;
            _paymentService = paymentService;
        }

        public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var domainEvent = domainEvents.Single();

            // state-machine

            if (domainEvent.EventType.FullName == "Invoice.Core.DomainModel.InvoiceCreatedEvent")
            {
                // Initiate process for this invoice
                dynamic invoiceCreated = domainEvent.GetAggregateEvent();
                Guid employeeId = invoiceCreated.EmployeeId;
                //Guid invoiceId = invoiceCreated.AggregateId;
                await _assignmentService.CreateAssignment(new CreateAssignmentCommand(AssignmentId.New, employeeId));
            }
            if (domainEvent.EventType.FullName == "Invoice.Core.DomainModel.InvoiceSentEvent")
            {
                // vanlig employee
                // skicka till payment??
                dynamic invoiceCreated = domainEvent.GetAggregateEvent();
                Guid invoiceId = invoiceCreated.InvoiceId;
                var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentForInvoiceQuery(invoiceId), CancellationToken.None);
                await _assignmentService.SetWaitingForPayment(new WaitForPaymentCommand(AssignmentId.With(assignment.AssignmentId), assignment.InvoiceId));
                await _paymentService.SetWaitingForPayment(new WaitingForPaymentRequest(){InvoiceId = invoiceId});
            }
            if (domainEvent.EventType.FullName == "Payment.Core.DomainModel.PaymentReceivedEvent")
            {
                // betalning mottagen, för vanlig employee, betala lön

            }

        }
        
    }
}
