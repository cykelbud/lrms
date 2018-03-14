using EventFlow.Aggregates;

namespace InvoiceService.Core.DomainModel
{
    public class InvoiceSentEvent : AggregateEvent<InvoiceAggregate, InvoiceId>
    {
    }
}