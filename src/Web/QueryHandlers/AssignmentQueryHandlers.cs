using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Core.ApplicationServices;
using Assignment.Response;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetAssignmentQueryHandler : IQueryHandler<GetAssignmentQuery, AssignmentDto>
    {
        private readonly IMssqlReadModelStore<AssignmentReadModel> _readStore;

        public GetAssignmentQueryHandler(IMssqlReadModelStore<AssignmentReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<AssignmentDto> ExecuteQueryAsync(GetAssignmentQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.AssignmentId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToAssignmentDto();
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

    //GetAssignmentForInvoiceQuery
    public class GetAssignmentForInvoiceQueryHandler : IQueryHandler<GetAssignmentByInvoiceIdQuery, AssignmentDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAssignmentForInvoiceQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<AssignmentDto> ExecuteQueryAsync(GetAssignmentByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var h = new GetAllAssignmentsQueryHandler(_msSqlConnection);
            var allDtos = await h.ExecuteQueryAsync(new GetAllAssignmentsQuery(), cancellationToken);
            var payment = allDtos.SingleOrDefault(p => p.InvoiceId == query.InvoiceId);
            return payment;
        }
    }

}
