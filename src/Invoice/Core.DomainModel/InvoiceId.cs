using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Invoice.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class InvoiceId : Identity<InvoiceId>
    {
        public InvoiceId(string value) : base(value)
        {
        }
    }
}
