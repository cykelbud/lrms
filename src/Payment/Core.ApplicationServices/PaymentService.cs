﻿using System;
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
    class PaymentService : IPaymentService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public PaymentService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }
        public async Task ReceivePayment(ReceivePaymentRequest request)
        {
            var payment = await _queryProcessor.ProcessAsync(new GetPaymentByInvoiceIdQuery(request.InvoiceId), CancellationToken.None);
            await _commandBus.PublishAsync(new ReceivePaymentCommand(PaymentId.With(payment.PaymentId), payment.InvoiceId), CancellationToken.None);
        }

        public async Task<IEnumerable<PaymentDto>> GetAll()
        {
            return await _queryProcessor.ProcessAsync(new GetAllPaymentsQuery(), CancellationToken.None);
        }

        public async Task PaymentDue(PaymentDueRequest request)
        {
            //_commandBus.PublishAsync(new PaymenrDueCommand(), CancellationToken.None);
        }

        public async Task SetWaitingForPayment(WaitingForPaymentRequest request)
        {
            await _commandBus.PublishAsync(new WaitForPaymentCommand(PaymentId.New, request.InvoiceId),
                CancellationToken.None);
        }
    }

}