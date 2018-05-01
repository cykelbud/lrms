using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;

namespace Employee.Core.DomainModel
{
    public class CreateCustomerCommand : Command<CustomerAggregate, CustomerId>
    {
        public Guid EmployeeId { get;  }
        public string UserName { get;  }
        public string PersonalIdentificationNumber { get;  }
        public string Address { get; }

        public CreateCustomerCommand(CustomerId aggregateId, Guid employeeId, string userName, string personalIdentificationNumber, string address) : base(aggregateId)
        {
            EmployeeId = employeeId;
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
            Address = address;
        }

        public CreateCustomerCommand(CustomerId aggregateId, ISourceId sourceId, Guid employeeId, string userName, string personalIdentificationNumber, string address) : base(aggregateId, sourceId)
        {
            EmployeeId = employeeId;
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
            Address = address;
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
