using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Invoice.Core.DomainModel;
using Invoice.Core.DomainServices;
using Invoice.Requests;
using Invoice.Response;

namespace Invoice.Core.ApplicationServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IInvoicePrinter _invoicePrinter;
        private readonly ISkatteverketService _skatteverketService;

        public InvoiceService(ICommandBus commandBus, IQueryProcessor queryProcessor, IInvoicePrinter invoicePrinter, ISkatteverketService skatteverketService)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _invoicePrinter = invoicePrinter;
            _skatteverketService = skatteverketService;
        }

        public async Task<Guid> CreateInvoice(CreateInvoiceRequest request)
        {
            var invoiceItems = request.InvoiceItems.Select(ii => new InvoiceItem(){Description = ii.Description, Price = ii.Price} ).ToArray();
            var invoiceId = InvoiceId.New;
            await _commandBus.PublishAsync(
                new InvoiceCreateCommand(invoiceId, request.EmployeeId, request.CustomerId, request.StartDate,
                    request.EndDate, request.InvoiceDescription, request.Name, request.Vat, invoiceItems, request.PayInAdvance, request.HasTaxReduction),
                CancellationToken.None);
            return invoiceId.GetGuid();
        }

        public async Task SendInvoice(SendInvoiceRequest request)
        {
            // Get data stored in this service
            var invoice = await _queryProcessor.ProcessAsync(new GetInvoiceQuery(request.InvoiceId), CancellationToken.None);
            var invoiceEmployee = await _queryProcessor.ProcessAsync(new GetInvoiceEmployeeQuery(invoice.EmployeeId), CancellationToken.None);
            var invoiceCustomer = await _queryProcessor.ProcessAsync(new GetInvoiceCustomerQuery(invoice.CustomerId), CancellationToken.None);

            var amount = invoice.InvoiceItems.Sum(item => item.Price * invoice.Vat);
            if (invoice.HasTaxReduction)
            {
                var reductionAmount = amount * 0.30m;
                amount = amount * 0.70m;
                await _skatteverketService.ApplyForReduction(invoiceCustomer.Name, reductionAmount);
            }

            _invoicePrinter.PrintInvoice(invoiceEmployee.Name, invoiceCustomer.Address, amount);

            await _commandBus.PublishAsync(new InvoiceSendCommand(InvoiceId.With(request.InvoiceId)), CancellationToken.None);
        }

        public async Task SendReminder(Guid invoiceId)
        {
            var invoice = await _queryProcessor.ProcessAsync(new GetInvoiceQuery(invoiceId), CancellationToken.None);
            var invoiceEmployee = await _queryProcessor.ProcessAsync(new GetInvoiceEmployeeQuery(invoice.EmployeeId), CancellationToken.None);
            var invoiceCustomer = await _queryProcessor.ProcessAsync(new GetInvoiceCustomerQuery(invoice.CustomerId), CancellationToken.None);
            _invoicePrinter.PrintReminder(invoiceEmployee.Name, invoiceCustomer.Address);
            await _commandBus.PublishAsync(new InvoiceReminderCommand(InvoiceId.With(invoiceId)), CancellationToken.None);
        }

        public async Task<InvoiceDto> GetInvoice(Guid invoiceId)
        {
            return await _queryProcessor.ProcessAsync(new GetInvoiceQuery(invoiceId), CancellationToken.None);
        }
    }
}