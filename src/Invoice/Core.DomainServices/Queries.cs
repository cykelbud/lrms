using System;
using EventFlow.Queries;
using Invoice.Response;

namespace Invoice.Core.DomainServices
{
    public class GetAllInvoicesQuery : IQuery<InvoiceDto[]>
    {
    }

    public class GetInvoiceQuery : IQuery<InvoiceDto>
    {
        public Guid Id { get; }

        public GetInvoiceQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetInvoiceCustomerQuery : IQuery<InvoiceCustomerDto>
    {
        public Guid Id { get; }

        public GetInvoiceCustomerQuery(Guid id)
        {
            Id = id;
        }
    }
    public class GetInvoiceEmployeeQuery : IQuery<InvoiceEmployeeDto>
    {
        public Guid Id { get; }

        public GetInvoiceEmployeeQuery(Guid id)
        {
            Id = id;
        }
    }


}