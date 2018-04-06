using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Payout.Core.DomainModel
{
    public class PayEmployeeCommand : Command<PayoutAggregate, PayoutId>
    {
        public Guid InvoiceId { get; }
        public decimal Amount { get;  }
        public DateTime PayoutDate { get; }
        public Guid EmployeeId { get; }


        public PayEmployeeCommand(PayoutId aggregateId, Guid invoiceId, decimal amount, DateTime payoutDate, Guid employeeId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            PayoutDate = payoutDate;
            EmployeeId = employeeId;
        }
    }

    public class PayEmployeeCommandHandler : CommandHandler<PayoutAggregate, PayoutId, PayEmployeeCommand>
    {
        public override Task ExecuteAsync(PayoutAggregate aggregate, PayEmployeeCommand command, CancellationToken cancellationToken)
        {
            aggregate.PayEmployee(command);
            return Task.CompletedTask;
        }
    }
}