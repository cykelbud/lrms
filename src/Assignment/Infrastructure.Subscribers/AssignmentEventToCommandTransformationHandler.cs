using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Core.DomainModel;
using Employee.Core.ApplicationServices;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using Invoice.Core.ApplicationServices;
using Payment.Core.ApplicationServices;
using Payment.Requests;
using Payout.Core.ApplicationServices;
using Payout.Requests;

namespace Assignment.Infrastructure.Subscribers
{
    public class AssignmentEventToCommandTransformationHandler : ISubscribeSynchronousToAll
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IPaymentService _paymentService;
        private readonly IPayoutService _payoutService;
        private readonly IEmployeeService _employeeService;
        private readonly IInvoiceService _invoiceService;

        public AssignmentEventToCommandTransformationHandler(
            IAssignmentService assignmentService, 
            IQueryProcessor queryProcessor,
            IPaymentService paymentService,
            IPayoutService payoutService,
            IEmployeeService employeeService,
            IInvoiceService invoiceService)
        {
            _assignmentService = assignmentService;
            _queryProcessor = queryProcessor;
            _paymentService = paymentService;
            _payoutService = payoutService;
            _employeeService = employeeService;
            _invoiceService = invoiceService;
        }

        public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var domainEvent = domainEvents.Single();

            // state-machine

            if (domainEvent.EventType.FullName == "Invoice.Core.DomainModel.InvoiceCreatedEvent")
            {
                // Initiate process for this invoice
                dynamic invoiceCreated = domainEvent.GetAggregateEvent();
                var id = domainEvent.GetIdentity();
                Guid invoiceId = Guid.Parse(id.Value.Replace("invoice-", ""));
                await _assignmentService.CreateAssignment(new CreateAssignmentCommand(AssignmentId.New, invoiceId));
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
                dynamic paymentReceived = domainEvent.GetAggregateEvent();
                Guid invoiceId = paymentReceived.InvoiceId;
                // betalning mottagen, för vanlig employee, betala lön
                var request = new PayEmployeeRequest {InvoiceId = invoiceId};
                await _payoutService.PayEmployee(request);
            //}
            //if (domainEvent.EventType.FullName == "Payout.Core.DomainModel.EmployeePaidEvent")
            //{
                // close assignment
                //dynamic employeePaid = domainEvent.GetAggregateEvent();
               // Guid invoiceId = employeePaid.InvoiceId;
                var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentForInvoiceQuery(invoiceId), CancellationToken.None);
                await _assignmentService.CloseAssignment(new CloseAssignmentCommand(AssignmentId.With(assignment.AssignmentId)));
            }

        }
        
    }
}
