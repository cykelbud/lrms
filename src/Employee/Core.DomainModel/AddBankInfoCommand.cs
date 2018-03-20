using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.Core;

namespace Employee.Core.DomainModel
{
    public class AddBankInfoCommand : Command<EmployeeAggregate, EmployeeId>
    {
        public string BankAccountNumber { get; }

        public AddBankInfoCommand(EmployeeId aggregateId, string bankAccountNumber) : base(aggregateId)
        {
            BankAccountNumber = bankAccountNumber;
        }

        public AddBankInfoCommand(EmployeeId aggregateId, ISourceId sourceId, string bankAccountNumber) : base(aggregateId, sourceId)
        {
            BankAccountNumber = bankAccountNumber;
        }
    }

    public class AddBankInfoCommandHandler : CommandHandler<EmployeeAggregate, EmployeeId, AddBankInfoCommand>
    {
        public override Task ExecuteAsync(EmployeeAggregate aggregate, AddBankInfoCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddBankInfo(command);
            return Task.FromResult(0);
        }
    }
}