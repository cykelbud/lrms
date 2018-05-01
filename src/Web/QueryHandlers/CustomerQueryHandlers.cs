using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Employee.Core.DomainServices;
using Employee.Response;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using Web.Projections;

namespace Web.QueryHandlers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, CustomerDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllCustomersQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<CustomerDto[]> ExecuteQueryAsync(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<CustomerReadModel>(
                    Label.Named("mssql-fetch-loanapp-read-model"),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Customer]")
                .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToCustomerDto()).ToArray();
        }

        
    }
}