using CustomerService.Core.DomainModel;
using EventFlow.Commands;
using EventFlow.Core;

namespace CustomerService.Commands
{
    internal class CreateCustomerCommand : Command<CustomerAggregate, CustomerId>
    {
        public string UserName { get;  }
        public string PersonalIdentificationNumber { get;  }

        public CreateCustomerCommand(CustomerId aggregateId, string userName, string personalIdentificationNumber) : base(aggregateId)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }

        public CreateCustomerCommand(CustomerId aggregateId, ISourceId sourceId, string userName, string personalIdentificationNumber) : base(aggregateId, sourceId)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }
    }
}
