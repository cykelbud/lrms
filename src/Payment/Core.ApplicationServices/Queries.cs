using System;
using System.Collections.Generic;
using EventFlow.Queries;
using Payment.Response;

namespace Payment.Core.ApplicationServices
{
    public class GetAllPaymentsQuery : IQuery<IEnumerable<PaymentDto>>
    {
    }

    public class GetPaymentQuery : IQuery<PaymentDto>
    {
        public Guid PaymentId { get; }

        public GetPaymentQuery(Guid paymentId)
        {
            PaymentId = paymentId;
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