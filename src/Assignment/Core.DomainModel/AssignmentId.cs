using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Assignment.Core.DomainModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class AssignmentId : Identity<AssignmentId>
    {
        public AssignmentId(string value) : base(value)
        {
        }
    }
}
