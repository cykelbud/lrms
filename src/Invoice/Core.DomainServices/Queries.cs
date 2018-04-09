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
        public Guid InvoiceId { get; }

        public GetInvoiceQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class GetInvoiceCustomerQuery : IQuery<InvoiceCustomerDto>
    {
        public Guid CustomerId { get; }

        public GetInvoiceCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    public class GetInvoiceEmployeeQuery : IQuery<InvoiceEmployeeDto>
    {
        public Guid EmployeeId { get; }

        public GetInvoiceEmployeeQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }


}