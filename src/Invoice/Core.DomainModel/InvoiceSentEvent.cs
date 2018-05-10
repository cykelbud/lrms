using System;
using EventFlow.Aggregates;

namespace Invoice.Core.DomainModel
{
    public class InvoiceSentEvent : AggregateEvent<InvoiceAggregate, InvoiceId>
    {
        public DateTime InvoiceSentDate { get; }

        public InvoiceSentEvent(DateTime invoiceSentDate)
        {
            InvoiceSentDate = invoiceSentDate;
        }
    }
}