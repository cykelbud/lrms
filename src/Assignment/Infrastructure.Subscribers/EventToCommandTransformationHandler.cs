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
using Invoice.Core.ApplicationServices;
using Payment.Core.ApplicationServices;
using Payment.Requests;
using Payout.Core.ApplicationServices;
using Payout.Requests;

namespace Assignment.Infrastructure.Subscribers
{
    public class EventToCommandTransformationHandler : ISubscribeSynchronousToAll
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IPaymentService _paymentService;
        private readonly IPayoutService _payoutService;
        private readonly IInvoiceService _invoiceService;

        public EventToCommandTransformationHandler(
            IAssignmentService assignmentService,
            IQueryProcessor queryProcessor,
            IPaymentService paymentService,
            IPayoutService payoutService,
            IInvoiceService invoiceService)
        {
            _assignmentService = assignmentService;
            _queryProcessor = queryProcessor;
            _paymentService = paymentService;
            _payoutService = payoutService;
            _invoiceService = invoiceService;
        }

        public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var domainEvent = domainEvents.Single();

            switch (domainEvent.EventType.FullName)
            {
                case "Invoice.Core.DomainModel.InvoiceCreatedEvent":
                    {
                        // Initiate process for this invoice
                        var id = domainEvent.GetIdentity();
                        var invoiceId = Guid.Parse(id.Value.Replace("invoice-", ""));
                        await _assignmentService.CreateAssignment(new CreateAssignmentCommand(AssignmentId.New, invoiceId));
                        break;
                    }
                case "Invoice.Core.DomainModel.InvoiceSentEvent":
                    {
                        var id = domainEvent.GetIdentity();
                        var invoiceId = Guid.Parse(id.Value.Replace("invoice-", ""));

                        var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentByInvoiceIdQuery(invoiceId), CancellationToken.None);
                        await _assignmentService.SetWaitingForPayment(new SetWaitingForPaymentCommand(AssignmentId.With(assignment.AssignmentId), assignment.InvoiceId));
                        await _paymentService.SetWaitingForPayment(new WaitingForPaymentRequest() { InvoiceId = invoiceId });

                        var assignmentInvoice = await _queryProcessor.ProcessAsync(new GetAssignmentInvoiceQuery(invoiceId), CancellationToken.None);
                        if (assignmentInvoice.PayInAdvance)
                        {
                            await _payoutService.PayEmployee(new PayEmployeeRequest { InvoiceId = invoiceId });
                        }

                        break;
                    }
                case "Payment.Core.DomainModel.PaymentReceivedEvent":
                {
                    dynamic paymentReceived = domainEvent.GetAggregateEvent();
                    Guid invoiceId = paymentReceived.InvoiceId;
                    var assignmentInvoice = await _queryProcessor.ProcessAsync(new GetAssignmentInvoiceQuery(invoiceId), CancellationToken.None);
                    if (!assignmentInvoice.PayInAdvance)
                    {
                        await _payoutService.PayEmployee(new PayEmployeeRequest { InvoiceId = invoiceId });
                    }
                    break;
                }
                case "Payment.Core.DomainModel.PaymentDueEvent":
                    {
                        dynamic paymentDue = domainEvent.GetAggregateEvent();
                        Guid invoiceId = paymentDue.InvoiceId;
                        await _invoiceService.SendReminder(invoiceId);
                        break;
                    }
                case "Payout.Core.DomainModel.EmployeePaidEvent":
                    {
                        // close assignment
                        dynamic employeePaid = domainEvent.GetAggregateEvent();
                        Guid invoiceId = employeePaid.InvoiceId;
                        var assignment = await _queryProcessor.ProcessAsync(new GetAssignmentByInvoiceIdQuery(invoiceId), CancellationToken.None);
                        await _assignmentService.CloseAssignment(new CloseAssignmentCommand(AssignmentId.With(assignment.AssignmentId)));
                        break;
                    }
            }
        }

    }
}
