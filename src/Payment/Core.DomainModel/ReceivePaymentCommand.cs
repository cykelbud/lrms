using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Payment.Core.DomainModel
{
    public class ReceivePaymentCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }
        public decimal Amount { get; }

        public ReceivePaymentCommand(PaymentId aggregateId, Guid invoiceId, decimal amount) : base(aggregateId)
        {
            InvoiceId = invoiceId;
            Amount = amount;
        }
    }

    public class ReceivePaymentCommandHandler : CommandHandler<PaymentAggregate, PaymentId, ReceivePaymentCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, ReceivePaymentCommand command, CancellationToken cancellationToken)
        {
            aggregate.ReceivePayment(command);
            return Task.CompletedTask;
        }
    }


    public class PaymentDueCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public PaymentDueCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class PaymentDueCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PaymentDueCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, PaymentDueCommand command, CancellationToken cancellationToken)
        {
            aggregate.PaymentDue(command);
            return Task.CompletedTask;
        }
    }

    //
    public class DebtCollectionCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public DebtCollectionCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class DebtCollectionCommandHandler : CommandHandler<PaymentAggregate, PaymentId, DebtCollectionCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, DebtCollectionCommand command, CancellationToken cancellationToken)
        {
            aggregate.DebtCollection(command);
            return Task.CompletedTask;
        }
    }

    // 
    public class PaymentInjunctionCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public PaymentInjunctionCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class PaymentInjunctionCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PaymentInjunctionCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, PaymentInjunctionCommand command, CancellationToken cancellationToken)
        {
            aggregate.PaymentInjunction(command);
            return Task.CompletedTask;
        }
    }
    //
    public class DistraintCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public DistraintCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class DistraintCommandHandler : CommandHandler<PaymentAggregate, PaymentId, DistraintCommand>
    {
        public override Task ExecuteAsync(PaymentAggregate aggregate, DistraintCommand command, CancellationToken cancellationToken)
        {
            aggregate.Distraint(command);
            return Task.CompletedTask;
        }
    }
}