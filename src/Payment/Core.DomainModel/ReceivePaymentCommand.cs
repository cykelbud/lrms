﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Payment.Core.DomainModel
{
    public class ReceivePaymentCommand : Command<PaymentAggregate, PaymentId>
    {
        public Guid InvoiceId { get; }

        public ReceivePaymentCommand(PaymentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
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

    //PaymentDueCommand

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

}