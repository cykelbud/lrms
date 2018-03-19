using System.Threading;
using System.Threading.Tasks;
using Customer.Core.DomainModel;
using EventFlow.Commands;
using EventFlow.Core;

namespace Customer.Commands
{
    public class CreateCustomerCommand : Command<CustomerAggregate, CustomerId>
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

    public class CreateCustomerCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CreateCustomerCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            aggregate.CreateCustomer(command);
            return Task.FromResult(0);
        }
    }
}
