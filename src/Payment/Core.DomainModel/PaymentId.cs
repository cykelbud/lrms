using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Payment.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class PaymentId : Identity<PaymentId>
    {
        public PaymentId(string value) : base(value)
        {
        }
    }
}