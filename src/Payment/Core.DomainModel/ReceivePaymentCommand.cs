using System;
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
}