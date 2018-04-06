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

    public class GetPayoutEmployeeQuery : IQuery<PayoutEmployeeDto>
    {
        public Guid EmployeeId { get; }

        public GetPayoutEmployeeQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }

    public class GetPayoutInvoiceQuery : IQuery<PayoutInvoiceDto>
    {
        public Guid InvoiceId { get; }

        public GetPayoutInvoiceQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }


}