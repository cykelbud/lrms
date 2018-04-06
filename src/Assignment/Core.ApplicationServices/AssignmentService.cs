using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.DomainModel;
using Assignment.Infrastructure.Subscribers;
using Assignment.Response;
using EventFlow;
using EventFlow.Queries;

namespace Assignment.Core.ApplicationServices
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public AssignmentService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task CreateAssignment(CreateAssignmentCommand command)
        {
            await _commandBus.PublishAsync(command, CancellationToken.None);
        }

        public async Task<AssignmentDto[]> GetAll()
        {
            var a = await _queryProcessor.ProcessAsync(new GetAllAssignmentsQuery(), CancellationToken.None);
            return a;
        }

        public async Task SetWaitingForPayment(WaitForPaymentCommand command)
        {
            await _commandBus.PublishAsync(command, CancellationToken.None);
        }

        public async Task CloseAssignment(CloseAssignmentCommand command)
        {
            await _commandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}