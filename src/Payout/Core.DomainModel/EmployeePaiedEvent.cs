using System;
using EventFlow.Aggregates;

namespace Payout.Core.DomainModel
{
    public class EmployeePaiedEvent : AggregateEvent<PayoutAggregate, PayoutId>
    {
        public Guid InvoiceId { get; }
        public decimal Amount { get; }
        public DateTime PayoutDate { get; }

        public EmployeePaiedEvent(Guid invoiceId, decimal amount, DateTime payoutDate)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            PayoutDate = payoutDate;
        }
    }
}