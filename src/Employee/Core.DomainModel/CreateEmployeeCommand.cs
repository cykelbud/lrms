using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;

namespace Employee.Core.DomainModel
{
    public class CreateEmployeeCommand : Command<EmployeeAggregate, EmployeeId>
    {
        public string UserName { get;  }
        public string PersonalIdentificationNumber { get;  }

        public CreateEmployeeCommand(EmployeeId aggregateId, string userName, string personalIdentificationNumber) : base(aggregateId)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }

        public CreateEmployeeCommand(EmployeeId aggregateId, ISourceId sourceId, string userName, string personalIdentificationNumber) : base(aggregateId, sourceId)
        {
            UserName = userName;
            PersonalIdentificationNumber = personalIdentificationNumber;
        }
    }

    public class CreateEmployeeCommandHandler : EventFlow.Commands.CommandHandler<EmployeeAggregate, EmployeeId, AddBankInfoCommand>
    {
        public override Task ExecuteAsync(EmployeeAggregate aggregate, AddBankInfoCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddBankInfo(command);
            return Task.FromResult(0);
        }
    }
}
