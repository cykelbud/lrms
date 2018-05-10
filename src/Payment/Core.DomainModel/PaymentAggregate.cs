using System;
using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>,
        IApply<WaitingForPaymentEvent>,
        IApply<PaymentReceivedEvent>,
        IApply<PaymentDueEvent>,
        IApply<DebtCollectionEvent>,
        IApply<PaymentInjunctionEvent>,
        IApply<DistraintEvent>
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
        
        public void PaymentDue(PaymentDueCommand command)
        {
            Emit(new PaymentDueEvent(command.InvoiceId));
        }

        public void Apply(PaymentDueEvent aggregateEvent)
        {
            CurrentState = State.PaymentDue;
        }

        public void DebtCollection(DebtCollectionCommand command)
        {
            Emit(new DebtCollectionEvent(command.InvoiceId, DateTime.Now));
        }

        public void PaymentInjunction(PaymentInjunctionCommand command)
        {
            Emit(new PaymentInjunctionEvent(command.InvoiceId, DateTime.Now));
        }

        public void Distraint(DistraintCommand command)
        {
            Emit(new DistraintEvent(command.InvoiceId, DateTime.Now));
        }

        public void Apply(DebtCollectionEvent aggregateEvent)
        {
            CurrentState = State.DebtCollection;
        }

        public void Apply(PaymentInjunctionEvent aggregateEvent)
        {
            CurrentState = State.PaymentInjuction;
        }

        public void Apply(DistraintEvent aggregateEvent)
        {
            CurrentState = State.Distraint;
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