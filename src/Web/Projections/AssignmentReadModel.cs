using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Core.DomainModel;
using Assignment.Response;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Newtonsoft.Json;

namespace Web.Projections
{
    [Table("ReadModel-Assignment")]
    public class AssignmentReadModel : IReadModel,
        IAmReadModelFor<AssignmentAggregate, AssignmentId, AssignmentCreatedEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }

        public string Json { get; set; }

        public AssignmentDto ToAssignmentDto()
        {
            var assignment = JsonConvert.DeserializeObject<AssignmentDto>(Json);
            return assignment;
        }

        public void Apply(IReadModelContext context, IDomainEvent<AssignmentAggregate, AssignmentId, AssignmentCreatedEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.GetGuid().ToString("D");
            var assignment = new AssignmentDto()
            {
                EmployeeId = domainEvent.AggregateIdentity.GetGuid()
            };

            Json = JsonConvert.SerializeObject(assignment);
        }

      
    }
}
