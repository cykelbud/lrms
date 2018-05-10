using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Assignment.Core.DomainModel
{
    public class SetWaitingForPaymentCommand : Command<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public SetWaitingForPaymentCommand(AssignmentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class WaitForPaymentCommandHandler : CommandHandler<AssignmentAggregate, AssignmentId, SetWaitingForPaymentCommand>
    {
        public override Task ExecuteAsync(AssignmentAggregate aggregate, SetWaitingForPaymentCommand command, CancellationToken cancellationToken)
        {
            aggregate.WaitForPayment(command);
            return Task.CompletedTask;
        }
    }
}

