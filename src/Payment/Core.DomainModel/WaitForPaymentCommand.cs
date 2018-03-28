using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Payment.Core.DomainModel
{
    public class WaitForPaymentCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public WaitForPaymentCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class WaitForPaymentCommandHandler : CommandHandler<PaymentAggregate, PaymentId, WaitForPaymentCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, WaitForPaymentCommand command, CancellationToken cancellationToken)
        {
            aggregate.WaitForPayment(command);
            return Task.CompletedTask;
        }
    }
}