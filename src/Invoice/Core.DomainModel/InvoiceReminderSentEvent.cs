using System;
using EventFlow.Aggregates;

namespace Invoice.Core.DomainModel
{
    public class InvoiceReminderSentEvent : AggregateEvent<InvoiceAggregate, InvoiceId>
    {
        public DateTime ReminderSentDate { get; }

        public InvoiceReminderSentEvent(DateTime reminderSentDate)
        {
            ReminderSentDate = reminderSentDate;
        }
    }
}