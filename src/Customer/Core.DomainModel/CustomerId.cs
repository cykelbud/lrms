using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Customer.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CustomerId : Identity<CustomerId>
    {
        public CustomerId(string value) : base(value)
        {
        }
    }
}
