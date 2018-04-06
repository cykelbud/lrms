using System;
using EventFlow.Aggregates;

namespace Payout.Core.DomainModel
{
    public class EmployeePaidEvent : AggregateEvent<PayoutAggregate, PayoutId>
    {
        public Guid InvoiceId { get; }
        public decimal Amount { get; }
        public DateTime PayoutDate { get; }
        public Guid EmployeeId { get; }

        public EmployeePaidEvent(Guid invoiceId, decimal amount, DateTime payoutDate, Guid employeeId)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            PayoutDate = payoutDate;
            EmployeeId = employeeId;
        }
    }
}