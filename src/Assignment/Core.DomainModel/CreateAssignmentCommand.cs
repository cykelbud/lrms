using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Assignment.Core.DomainModel
{
    public class CreateAssignmentCommand : Command<AssignmentAggregate, AssignmentId>
    {
        public Guid EmployeeId { get; }

        public CreateAssignmentCommand(AssignmentId aggregateId, Guid employeeId) : base(aggregateId)
        {
            EmployeeId = employeeId;
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
