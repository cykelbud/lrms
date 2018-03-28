using System;
using EventFlow.Queries;
using Payout.Response;

namespace Payout.Core.ApplicationServices
{
    public class GetAllPayoutQuery : IQuery<PayoutDto[]>
    {
    }

    public class GetPayoutQuery : IQuery<PayoutDto>
    {
        public Guid Id { get; }

        public GetPayoutQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPayoutByInvoiceIdQuery : IQuery<PayoutDto>
    {
        public Guid InvoiceId { get; }

        public GetPayoutByInvoiceIdQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }



}