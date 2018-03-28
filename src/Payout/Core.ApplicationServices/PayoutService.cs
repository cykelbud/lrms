using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Payout.Core.DomainModel;
using Payout.Requests;
using Payout.Response;

namespace Payout.Core.ApplicationServices
{
    internal class PayoutService : IPayoutService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public PayoutService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }
     

        public async Task<PayoutDto[]> GetAll()
        {
            return null;
            //return await _queryProcessor.ProcessAsync(new GetAllPaymentsQuery(), CancellationToken.None);
        }



        public async Task PayEmployee(PayEmployeeRequest request)
        {
            // make payment to lets say a bank

            await _commandBus.PublishAsync(new PayEmployeeCommand(PayoutId.New, request.InvoiceId, 0, DateTime.Now),
                CancellationToken.None);
        }
    }
}