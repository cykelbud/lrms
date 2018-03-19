using EventFlow.Aggregates;

namespace Payment.Core.DomainModel
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {
        // state
        public string UserName { get; set; }
        public string PersonalIdentificationNumber { get; set; }
        public string BankAccountNumber { get; set; }


        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

     
    }
}
