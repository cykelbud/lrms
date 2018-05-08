using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Payment.Core.DomainModel;
using Payment.Requests;
using Payment.Response;

namespace Payment.Core.ApplicationServices
{
    internal class PaymentService : IPaymentService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public PaymentService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<IEnumerable<PaymentDto>> GetAll()
        {
            return await _queryProcessor.ProcessAsync(new GetAllPaymentsQuery(), CancellationToken.None);
        }

        public async Task SimulateReceivePayment(ReceivePaymentRequest request)
        {
            var payment = await _queryProcessor.ProcessAsync(new GetPaymentByInvoiceIdQuery(request.InvoiceId), CancellationToken.None);
            if (payment == null)
            {
                throw new ArgumentException($"No payment found for invoice {request.InvoiceId}");
            }
            await _commandBus.PublishAsync(new ReceivePaymentCommand(PaymentId.With(payment.PaymentId), payment.InvoiceId), CancellationToken.None);
        }

        public async Task SimulatePaymentDue(PaymentDueRequest request)
        {
            var payment = await _queryProcessor.ProcessAsync(new GetPaymentByInvoiceIdQuery(request.InvoiceId), CancellationToken.None);
            if (payment == null)
            {
                throw new ArgumentException($"No payment found for invoice {request.InvoiceId}");
            }
            await _commandBus.PublishAsync(new PaymentDueCommand(PaymentId.With(payment.PaymentId), payment.InvoiceId), CancellationToken.None);
        }

        public async Task SetWaitingForPayment(WaitingForPaymentRequest request)
        {
            // creates a new payment
            await _commandBus.PublishAsync(new WaitForPaymentCommand(PaymentId.New, request.InvoiceId), CancellationToken.None);
        }
    }

}