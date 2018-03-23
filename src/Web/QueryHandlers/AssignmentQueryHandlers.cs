using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Response;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetAssignmentQueryHandler : IQueryHandler<GetAssignmentQuery, AssignmentDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAssignmentQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<AssignmentDto> ExecuteQueryAsync(GetAssignmentQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<AssignmentReadModel>(
                    Label.Named(nameof(GetAssignmentQueryHandler)),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Assignment]")
                .ConfigureAwait(false);
            return readModels.Where(assignment => assignment.AggregateId == query.Id.ToString("D")).Select(rm => rm.ToAssignmentDto()).SingleOrDefault();
        }
    }

    public class GetAllAssignmentsQueryHandler : IQueryHandler<GetAllAssignmentsQuery, AssignmentDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllAssignmentsQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<AssignmentDto[]> ExecuteQueryAsync(GetAllAssignmentsQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<AssignmentReadModel>(
                        Label.Named(nameof(GetAllAssignmentsQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Assignment]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToAssignmentDto()).ToArray();
        }
    }
    
}
