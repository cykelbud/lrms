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

    public class GetInvoiceQueryHandler : IQueryHandler<GetInvoiceQuery, InvoiceDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetInvoiceQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<InvoiceDto> ExecuteQueryAsync(GetInvoiceQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<InvoiceReadModel>(
                    Label.Named(nameof(GetInvoiceQueryHandler)),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Invoice]")
                .ConfigureAwait(false);
            return readModels.Where(invoice => invoice.AggregateId == query.Id.ToString("D")).Select(rm => rm.ToInvoiceDto()).SingleOrDefault();
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
        private readonly IMsSqlConnection _msSqlConnection;

        public GetInvoiceCustomerQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }
        public async Task<InvoiceCustomerDto> ExecuteQueryAsync(GetInvoiceCustomerQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<CustomerReadModel>(
                    Label.Named(nameof(GetInvoiceCustomerQueryHandler)),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Customer]")
                .ConfigureAwait(false);
            return readModels.Where(ic => ic.AggregateId == query.Id.ToString("D")).Select(rm => rm.ToInvoiceCustomerDto()).SingleOrDefault();
        }
    }

    public class GetInvoiceEmployeeQueryHandler : IQueryHandler<GetInvoiceEmployeeQuery, InvoiceEmployeeDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetInvoiceEmployeeQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<InvoiceEmployeeDto> ExecuteQueryAsync(GetInvoiceEmployeeQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<EmployeeReadModel>(
                    Label.Named(nameof(GetInvoiceEmployeeQueryHandler)),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Employee]")
                .ConfigureAwait(false);
            return readModels.Where(ic => ic.AggregateId == query.Id.ToString("D")).Select(rm => rm.ToInvoiceEmployeeDto()).SingleOrDefault();
        }
    }
}
