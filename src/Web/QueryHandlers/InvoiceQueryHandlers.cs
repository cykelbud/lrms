using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using Invoice.Core.DomainServices;
using Invoice.Response;
using Web.Projections;

namespace Web.QueryHandlers
{


    public class GetAllInvoicesQueryHandler : IQueryHandler<GetAllInvoicesQuery, InvoiceDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllInvoicesQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<InvoiceDto[]> ExecuteQueryAsync(GetAllInvoicesQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<InvoiceReadModel>(
                        Label.Named("mssql-fetch-loanapp-read-model"),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Employee]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToInvoiceDto()).ToArray();
        }
    }
}
