using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using Payment.Core.ApplicationServices;
using Payment.Response;
using Web.Projections;

namespace Web.QueryHandlers
{

    public class GetPaymentQueryHandler : IQueryHandler<GetPaymentQuery, PaymentDto>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetPaymentQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<PaymentDto> ExecuteQueryAsync(GetPaymentQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<PaymentReadModel>(
                    Label.Named(nameof(GetPaymentQueryHandler)),
                    cancellationToken,
                    "SELECT * FROM [ReadModel-Payment]")
                .ConfigureAwait(false);
            return readModels.Where(payment => payment.AggregateId == query.Id.ToString("D")).Select(rm => rm.ToPaymentDto()).SingleOrDefault();
        }
    }

    public class GetAllPaymentsQueryHandler : IQueryHandler<GetAllPaymentsQuery, PaymentDto[]>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllPaymentsQueryHandler(IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public async Task<PaymentDto[]> ExecuteQueryAsync(GetAllPaymentsQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<PaymentReadModel>(
                        Label.Named(nameof(GetAllInvoicesQueryHandler)),
                        cancellationToken,
                        "SELECT * FROM [ReadModel-Payment]")
                    .ConfigureAwait(false);
            return readModels.Select(rm => rm.ToPaymentDto()).ToArray();
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
