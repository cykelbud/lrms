using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>,
        IApply<WaitingForPaymentEvent>
    {
        // state
        public Guid InvoiceId { get; set; }
        public State CurrentState { get; set; }

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
    }

    public enum State
    {
        WaitingForPayment,
        PaymentDue,
        DebtCollection, // inkasso
        PaymentInjuction, // betalnings föreläggande
        Distraint, // utmätning
    }
}