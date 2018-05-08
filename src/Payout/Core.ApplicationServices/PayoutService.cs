using System;
using System.Collections.Generic;
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
        private readonly IMarginalenBank _marginalenBank;
        private readonly ISkatteverket _skatteverket;

        public PayoutService(ICommandBus commandBus, IQueryProcessor queryProcessor, IMarginalenBank marginalenBank, ISkatteverket skatteverket)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _marginalenBank = marginalenBank;
            _skatteverket = skatteverket;
        }
     
        public async Task<IEnumerable<PayoutDto>> GetAll()
        {
            return await _queryProcessor.ProcessAsync(new GetAllPayoutQuery(), CancellationToken.None).ConfigureAwait(false);
        }
        
        public async Task PayEmployee(PayEmployeeRequest request)
        {
            // Immediate wage payout to employee, no flow.
            
            // get amount from invoice
            var payoutInvoice = await _queryProcessor.ProcessAsync(new GetPayoutInvoiceQuery(request.InvoiceId), CancellationToken.None);
            // get bankaccount number from employee
            var payoutEmployee = await _queryProcessor.ProcessAsync(new GetPayoutEmployeeQuery(payoutInvoice.EmployeeId), CancellationToken.None);

            // make payment to lets say a bank
            var commission = payoutInvoice.Amount * 0.05m; // 5% commission, bruttolön
            var afterCommission = payoutInvoice.Amount - commission;
            var bruttolön = afterCommission / 1.3142m; // 31.42 arbetsgivaravgift 
            var afterTax = bruttolön * 0.70m; // 30% schablonskatt på egenanställning

            var accountNo = payoutEmployee.BankAccountNumber;
            var totalTax = afterCommission - afterTax;

            await _marginalenBank.Pay(accountNo, afterTax);
            await _skatteverket.Pay("ocr", totalTax);

            // store this command to raise an event to others
            await _commandBus.PublishAsync(new PayEmployeeCommand(PayoutId.New, request.InvoiceId, afterTax, DateTime.Now, payoutInvoice.EmployeeId), CancellationToken.None);
        }
    }
}