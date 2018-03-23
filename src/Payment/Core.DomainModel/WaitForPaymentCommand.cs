using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Payment.Core.DomainModel
{
    public class WaitForPaymentCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; set; }

        public WaitForPaymentCommand(PaymentId aggregateId) : base(aggregateId)
        {
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