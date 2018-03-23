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


}