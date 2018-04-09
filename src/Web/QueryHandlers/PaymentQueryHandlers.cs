using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Payment.Core.ApplicationServices;
using Payment.Response;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetPaymentQueryHandler : IQueryHandler<GetPaymentQuery, PaymentDto>
    {
        private readonly IMssqlReadModelStore<PaymentReadModel> _readStore;

        public GetPaymentQueryHandler(IMssqlReadModelStore<PaymentReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<PaymentDto> ExecuteQueryAsync(GetPaymentQuery query, CancellationToken cancellationToken)
        {
            var invoiceId = query.PaymentId.ToString("D");
            var readModel = await _readStore.GetAsync(invoiceId, cancellationToken).ConfigureAwait(false);
            return readModel.ReadModel.ToPaymentDto();
        }
    }

    public class GetAllPaymentsQueryHandler : IQueryHandler<GetAllPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllPaymentsQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<IEnumerable<PaymentDto>> ExecuteQueryAsync(GetAllPaymentsQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<PaymentReadModel>(
                        Label.Named(nameof(GetAllInvoicesQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Payment]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToPaymentDto());
        }
    }

    public class GetPaymentFromInvoiceIdQueryHandler : IQueryHandler<GetPaymentByInvoiceIdQuery, PaymentDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetPaymentFromInvoiceIdQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<PaymentDto> ExecuteQueryAsync(GetPaymentByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var h = new GetAllPaymentsQueryHandler(_msSqlConnection);
            var allDtos = await h.ExecuteQueryAsync(new GetAllPaymentsQuery(), cancellationToken);
            var payment = allDtos.SingleOrDefault(p => p.InvoiceId == query.InvoiceId);
            return payment;
        }
    }

}
