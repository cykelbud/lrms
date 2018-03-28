using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>,
        IApply<WaitingForPaymentEvent>,
        IApply<PaymentReceivedEvent>
    {
        // state
        public Guid InvoiceId { get; set; }
        public State CurrentState { get; set; }
        public DateTime PaymentReceivedDate { get; set; }

        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void WaitForPayment(WaitForPaymentCommand command)
        {
            Emit(new WaitingForPaymentEvent(command.InvoiceId));
        }

        public void Apply(WaitingForPaymentEvent aggregateEvent)
        {
            InvoiceId = aggregateEvent.InvoiceId;
            CurrentState = State.WaitingForPayment;
        }

        public void ReceivePayment(ReceivePaymentCommand command)
        {
            Emit(new PaymentReceivedEvent(command.InvoiceId, DateTime.Now));
        }

        public void Apply(PaymentReceivedEvent aggregateEvent)
        {
            PaymentReceivedDate = aggregateEvent.ReceivedDate;
            CurrentState = State.PaymentReceived;
        }

    }

    public enum State
    {
        WaitingForPayment,
        PaymentReceived,
        PaymentDue,
        DebtCollection, // inkasso
        PaymentInjuction, // betalnings föreläggande
        Distraint, // utmätning
    }
}