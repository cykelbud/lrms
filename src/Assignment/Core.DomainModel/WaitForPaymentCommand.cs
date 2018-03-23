using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Assignment.Core.DomainModel
{
    public class WaitForPaymentCommand : Command<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public WaitForPaymentCommand(AssignmentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class WaitForPaymentCommandHandler : CommandHandler<AssignmentAggregate, AssignmentId, WaitForPaymentCommand>
    {
        public override Task ExecuteAsync(AssignmentAggregate aggregate, WaitForPaymentCommand command, CancellationToken cancellationToken)
        {
            aggregate.WaitForPayment(command);
            return Task.CompletedTask;
        }
    }
}

