﻿using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentAggregate : AggregateRoot<AssignmentAggregate, AssignmentId>,
        IApply<AssignmentCreatedEvent>,
        IApply<AssignmentInWaitingForPaymentStateEvent>,
        IApply<AssignmentClosedEvent>
    {
        // state
        public Guid InvoiceId { get; set; }
        public State CurrentState { get; set; }


        public AssignmentAggregate(AssignmentId id) : base(id)
        {
        }
        
        public void CreateAssignment(CreateAssignmentCommand command)
        {
            Emit(new AssignmentCreatedEvent(command.InvoiceId));
        }

        public void Apply(AssignmentCreatedEvent aggregateEvent)
        {
            InvoiceId = aggregateEvent.InvoiceId;
            CurrentState = State.ProcessingInvoice;
        }

        public void WaitForPayment(SetWaitingForPaymentCommand command)
        {
            Emit(new AssignmentInWaitingForPaymentStateEvent(command.InvoiceId));
        }

        public void Apply(AssignmentInWaitingForPaymentStateEvent aggregateStateEvent)
        {
            CurrentState = State.ProcessingPayment;
        }

        public void CloseAssignment(CloseAssignmentCommand command)
        {
            Emit(new AssignmentClosedEvent());
        }

        public void Apply(AssignmentClosedEvent aggregateEvent)
        {
            CurrentState = State.Closed;
        }
    }

    public enum State
    {
        ProcessingInvoice,
        ProcessingPayment,
        ProcessingPayout,
        Closed
    }
}
