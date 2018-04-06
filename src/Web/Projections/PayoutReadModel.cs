using System.ComponentModel.DataAnnotations.Schema;
using Payout.Core.DomainModel;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Newtonsoft.Json;
using Payout.Response;

namespace Web.Projections
{
    [Table("ReadModel-Payout")]
    public class PayoutReadModel : IReadModel,
        IAmReadModelFor<PayoutAggregate, PayoutId, EmployeePaidEvent>
    {
        [MsSqlReadModelIdentityColumn]
        public string AggregateId { get; set; }

        public string Json { get; set; }

        public PayoutDto ToPayoutDto()
        {
            var payout = JsonConvert.DeserializeObject<PayoutDto>(Json);
            return payout;
        }

        public void Apply(IReadModelContext context, IDomainEvent<PayoutAggregate, PayoutId, EmployeePaidEvent> domainEvent)
        {
            AggregateId = domainEvent.AggregateIdentity.GetGuid().ToString("D");
            var e = domainEvent.AggregateEvent;
            var payout = new PayoutDto()
            {
               PayoutId = domainEvent.AggregateIdentity.GetGuid(),
               InvoiceId = e.InvoiceId,
               EmployeeId = e.EmployeeId,
               Amount = e.Amount,
               Date = e.PayoutDate
            };

            Json = JsonConvert.SerializeObject(payout);
        }


      
    }
}
