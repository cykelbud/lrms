using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Employee.Core.ApplicationServices;
using Employee.Response;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using Web.Projections;

namespace Web.QueryHandlers
{


    public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, EmployeeDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllEmployeesQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<EmployeeDto[]> ExecuteQueryAsync(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<EmployeeReadModel>(
                        Label.Named(nameof(GetAllEmployeesQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Employee]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToEmployeeDto()).ToArray();
        }
    }
}
