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
            return await _queryProcessor.ProcessAsync(new GetAllPayoutQuery(), CancellationToken.None);
        }


        public async Task PayEmployee(PayEmployeeRequest request)
        {
            // Immediate wage payout to employee, no flow.

            // get employee from invoice
            var payout = await _queryProcessor.ProcessAsync(new GetPayoutByInvoiceIdQuery(request.InvoiceId), CancellationToken.None);
            // get bankaccount number from employee
            var payoutEmployee = await _queryProcessor.ProcessAsync(new GetPayoutEmployeeQuery(payout.EmployeeId), CancellationToken.None);
            // get amount from invoice
            var payoutInvoice = await _queryProcessor.ProcessAsync(new GetPayoutInvoiceQuery(request.InvoiceId), CancellationToken.None);

            // make payment to lets say a bank
            var amount = payoutInvoice.Amount * 0.95m; // 5% commission
            var accountNo = payoutEmployee.BankAccountNumber;
            // Todo

            // store this command to raise an event to others
            await _commandBus.PublishAsync(new PayEmployeeCommand(PayoutId.New, request.InvoiceId, amount, DateTime.Now, payout.EmployeeId),
                CancellationToken.None);
        }
    }

}