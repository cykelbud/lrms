using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;

namespace InvoiceService.Core.DomainModel
{
    public class InvoiceSendCommand : Command<InvoiceAggregate,InvoiceId>
    {
        public InvoiceSendCommand(InvoiceId aggregateId) : base(aggregateId)
        {
        }

        public InvoiceSendCommand(InvoiceId aggregateId, ISourceId sourceId) : base(aggregateId, sourceId)
        {
        }
    }

    public class InvoiceSendCommandHandler : CommandHandler<InvoiceAggregate, InvoiceId, InvoiceSendCommand>
    {
        public override Task ExecuteAsync(InvoiceAggregate aggregate, InvoiceSendCommand command, CancellationToken cancellationToken)
        {
            aggregate.SendInvoice(command);
            return Task.FromResult(0);
        }
    }
}