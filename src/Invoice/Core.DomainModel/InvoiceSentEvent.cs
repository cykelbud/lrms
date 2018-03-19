using EventFlow.Aggregates;

namespace Invoice.Core.DomainModel
{
    public class InvoiceSentEvent : AggregateEvent<InvoiceAggregate, InvoiceId>
    {
    }
}