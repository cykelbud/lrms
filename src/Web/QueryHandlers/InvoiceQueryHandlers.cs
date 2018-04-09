using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Invoice.Core.DomainServices;
using Invoice.Response;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetInvoiceQueryHandler : IQueryHandler<GetInvoiceQuery, InvoiceDto>
    {
        private readonly IMssqlReadModelStore<InvoiceReadModel> _readStore;

        public GetInvoiceQueryHandler(IMssqlReadModelStore<InvoiceReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<InvoiceDto> ExecuteQueryAsync(GetInvoiceQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.InvoiceId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToInvoiceDto();
        }
    }

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
                        Label.Named(nameof(GetAllInvoicesQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Invoice]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToInvoiceDto()).ToArray();
        }
    }

    public class GetInvoiceCustomerQueryHandler : IQueryHandler<GetInvoiceCustomerQuery, InvoiceCustomerDto>
    {
        private readonly IMssqlReadModelStore<CustomerReadModel> _readStore;

        public GetInvoiceCustomerQueryHandler(IMssqlReadModelStore<CustomerReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<InvoiceCustomerDto> ExecuteQueryAsync(GetInvoiceCustomerQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.CustomerId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToInvoiceCustomerDto();
        }
    }

    public class GetInvoiceEmployeeQueryHandler : IQueryHandler<GetInvoiceEmployeeQuery, InvoiceEmployeeDto>
    {
        private readonly IMssqlReadModelStore<EmployeeReadModel> _readStore;

        public GetInvoiceEmployeeQueryHandler(IMssqlReadModelStore<EmployeeReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<InvoiceEmployeeDto> ExecuteQueryAsync(GetInvoiceEmployeeQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.EmployeeId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToInvoiceEmployeeDto();
        }
    }
}
