using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Assignment.Core.DomainModel
{
    public class CreateAssignmentCommand : Command<AssignmentAggregate, AssignmentId>
    {
        public Guid InvoiceId { get; }

        public CreateAssignmentCommand(AssignmentId aggregateId, Guid invoiceId) : base(aggregateId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class CreateEmployeeCommandandler : CommandHandler<AssignmentAggregate, AssignmentId, CreateAssignmentCommand>
    {

        public override Task ExecuteAsync(AssignmentAggregate aggregate, CreateAssignmentCommand command, CancellationToken cancellationToken)
        {
            aggregate.CreateAssignment(command);
            return Task.CompletedTask;
        }
    }
}
