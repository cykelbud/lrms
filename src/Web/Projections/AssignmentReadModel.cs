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
        IAmReadModelFor<AssignmentAggregate, AssignmentId, AssignmentCreatedEvent>,
        IAmReadModelFor<AssignmentAggregate, AssignmentId, AssignmentClosedEvent>,
        IAmReadModelFor<AssignmentAggregate, AssignmentId, AssignmentInWaitingForPaymentStateEvent>
    {

        public AssignmentReadModel()
        {
            
        }

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
            AggregateId = domainEvent.AggregateIdentity.Value;
            var e = domainEvent.AggregateEvent;
            var assignment = new AssignmentDto()
            {
                AssignmentId = domainEvent.AggregateIdentity.GetGuid(),
                InvoiceId = e.InvoiceId,
                CurrentStatus = Status.Created
            };

            Json = JsonConvert.SerializeObject(assignment);
        }


        public void Apply(IReadModelContext context, IDomainEvent<AssignmentAggregate, AssignmentId, AssignmentInWaitingForPaymentStateEvent> domainEvent)
        {
            var dto = JsonConvert.DeserializeObject<AssignmentDto>(Json);
            dto.CurrentStatus = Status.WaitingForPaymentFromCustomer;
            Json = JsonConvert.SerializeObject(dto);
        }

        public void Apply(IReadModelContext context, IDomainEvent<AssignmentAggregate, AssignmentId, AssignmentClosedEvent> domainEvent)
        {
            var dto = JsonConvert.DeserializeObject<AssignmentDto>(Json);
            dto.CurrentStatus = Status.Closed;
            Json = JsonConvert.SerializeObject(dto);
        }
    }
}
