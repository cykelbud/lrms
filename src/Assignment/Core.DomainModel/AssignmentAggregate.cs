using System;
using EventFlow.Aggregates;

namespace Assignment.Core.DomainModel
{
    public class AssignmentAggregate : AggregateRoot<AssignmentAggregate, AssignmentId>,
        IApply<AssignmentCreatedEvent>,
        IApply<WaitingForPaymentEvent>
    {
        // state
        public Guid EmployeeId { get; set; }
        public Guid InvoiceId { get; set; }
        public State CurrentState { get; set; }


        public AssignmentAggregate(AssignmentId id) : base(id)
        {
        }
        
        public void CreateAssignment(CreateAssignmentCommand command)
        {
            Emit(new AssignmentCreatedEvent(command.EmployeeId));
        }

        public void Apply(AssignmentCreatedEvent aggregateEvent)
        {
            EmployeeId = aggregateEvent.EmployeeId;
            CurrentState = State.ProcessingInvoice;
        }

        public void WaitForPayment(WaitForPaymentCommand command)
        {
            Emit(new WaitingForPaymentEvent(command.InvoiceId));
        }

        public void Apply(WaitingForPaymentEvent aggregateEvent)
        {
            CurrentState = State.ProcessingPayment;
            InvoiceId = aggregateEvent.InvoiceId;
        }
    }

    public enum State
    {
        ProcessingInvoice,
        ProcessingPayment,
        ProcessingPayout,
        Completed
    }
}
