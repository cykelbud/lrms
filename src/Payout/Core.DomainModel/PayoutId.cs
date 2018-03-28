using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Payout.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class PayoutId : Identity<PayoutId>
    {
        public PayoutId(string value) : base(value)
        {
        }
    }
}