using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Payout.Core.ApplicationServices;
using Payout.Response;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetPayoutQueryHandler : IQueryHandler<GetPayoutQuery, PayoutDto>
    {
        private readonly IMssqlReadModelStore<PayoutReadModel> _readStore;

        public GetPayoutQueryHandler(IMssqlReadModelStore<PayoutReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<PayoutDto> ExecuteQueryAsync(GetPayoutQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.PayoutId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToPayoutDto();
        }

    }

    public class GetAllPayoutsQueryHandler : IQueryHandler<GetAllPayoutQuery, PayoutDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllPayoutsQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<PayoutDto[]> ExecuteQueryAsync(GetAllPayoutQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<PayoutReadModel>(
                        Label.Named(nameof(GetAllInvoicesQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Payout]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToPayoutDto()).ToArray();
        }
    }

    public class GetPayoutFromInvoiceIdQueryHandler : IQueryHandler<GetPayoutByInvoiceIdQuery, PayoutDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetPayoutFromInvoiceIdQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<PayoutDto> ExecuteQueryAsync(GetPayoutByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var h = new GetAllPayoutsQueryHandler(_msSqlConnection);
            var allDtos = await h.ExecuteQueryAsync(new GetAllPayoutQuery(), cancellationToken);
            var payout = allDtos.SingleOrDefault(p => p.InvoiceId == query.InvoiceId);
            return payout;
        }
    }


    public class GetPayoutEmployeeQueryHandler : IQueryHandler<GetPayoutEmployeeQuery, PayoutEmployeeDto>
    {
        private readonly IMssqlReadModelStore<EmployeeReadModel> _readStore;

        public GetPayoutEmployeeQueryHandler(IMssqlReadModelStore<EmployeeReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<PayoutEmployeeDto> ExecuteQueryAsync(GetPayoutEmployeeQuery query, CancellationToken cancellationToken)
        {
            var employeeId = query.EmployeeId.ToString("D");
            var readModel = await _readStore.GetAsync(employeeId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToPayoutEmployeeDto();
        }
    }

    public class GetPayoutInvoiceQueryHandler : IQueryHandler<GetPayoutInvoiceQuery, PayoutInvoiceDto>
    {
        private readonly IMssqlReadModelStore<InvoiceReadModel> _readStore;

        public GetPayoutInvoiceQueryHandler(IMssqlReadModelStore<InvoiceReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<PayoutInvoiceDto> ExecuteQueryAsync(GetPayoutInvoiceQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.InvoiceId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToPayoutInvoiceDto();
        }
    }
}
