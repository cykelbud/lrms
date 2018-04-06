using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Assignment.Core.DomainModel
{
    public class CloseAssignmentCommand : Command<AssignmentAggregate, AssignmentId>
    {
        public CloseAssignmentCommand(AssignmentId aggregateId) : base(aggregateId)
        {
        }
    }

    public class CloseAssignmentCommandHandler : CommandHandler<AssignmentAggregate, AssignmentId, CloseAssignmentCommand>
    {
        public override Task ExecuteAsync(AssignmentAggregate aggregate, CloseAssignmentCommand command, CancellationToken cancellationToken)
        {
            aggregate.CloseAssignment(command);
            return Task.CompletedTask;

        }
    }
}