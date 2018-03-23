using System;
using EventFlow.Queries;
using Payment.Response;

namespace Payment.Core.ApplicationServices
{
    public class GetAllPaymentsQuery : IQuery<PaymentDto[]>
    {
    }

    public class GetPaymentQuery : IQuery<PaymentDto>
    {
        public Guid Id { get; }

        public GetPaymentQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPaymentByInvoiceIdQuery : IQuery<PaymentDto>
    {
        public Guid InvoiceId { get; }

        public GetPaymentByInvoiceIdQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }



}