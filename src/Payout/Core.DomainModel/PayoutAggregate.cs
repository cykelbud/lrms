﻿using System;
using EventFlow.Aggregates;

namespace Payout.Core.DomainModel
{
    public class PayoutAggregate : AggregateRoot<PayoutAggregate, PayoutId>,
        IApply<EmployeePaidEvent>
    {
        // state
        public DateTime PayoutDate { get; set; }
        public decimal Amount { get; set; }
        public Guid InvoiceId { get; set; }

        public PayoutAggregate(PayoutId id) : base(id)
        {
        }


        public void PayEmployee(PayEmployeeCommand command)
        {
            Emit(new EmployeePaidEvent(command.InvoiceId, command.Amount, command.PayoutDate, command.EmployeeId));
        }

        public void Apply(EmployeePaidEvent aggregateEvent)
        {
            InvoiceId = aggregateEvent.InvoiceId;
            Amount  = aggregateEvent.Amount;
            PayoutDate = aggregateEvent.PayoutDate;
        }

    }
}