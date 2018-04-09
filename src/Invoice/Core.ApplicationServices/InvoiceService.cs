using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Invoice.Core.DomainModel;
using Invoice.Core.DomainServices;
using Invoice.Requests;

namespace Invoice.Core.ApplicationServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IInvoicePrinter _invoicePrinter;

        public InvoiceService(ICommandBus commandBus, IQueryProcessor queryProcessor, IInvoicePrinter invoicePrinter)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _invoicePrinter = invoicePrinter;
        }

        public async Task<Guid> CreateInvoice(CreateInvoiceRequest request)
        {
            var invoiceItems = request.InvoiceItems.Select(ii => new InvoiceItem(){Description = ii.Description, Price = ii.Price} ).ToArray();
            var invoiceId = InvoiceId.New;
            await _commandBus.PublishAsync(
                new InvoiceCreateCommand(invoiceId, request.EmployeeId, request.CustomerId, request.StartDate,
                    request.EndDate, request.InvoiceDescription, request.Name, request.Vat, invoiceItems),
                CancellationToken.None);
            return invoiceId.GetGuid();
        }

        public async Task SendInvoice(SendInvoiceRequest request)
        {
            // Initiate process in workflow

            // Get data stored in this service
            var invoice = await _queryProcessor.ProcessAsync(new GetInvoiceQuery(request.InvoiceId), CancellationToken.None);
            var invoiceEmployee = await _queryProcessor.ProcessAsync(new GetInvoiceEmployeeQuery(invoice.EmployeeId), CancellationToken.None);
            var invoiceCustomer = await _queryProcessor.ProcessAsync(new GetInvoiceCustomerQuery(invoice.CustomerId), CancellationToken.None);
            var amount = invoice.InvoiceItems.Sum(item => item.Price * invoice.Vat);

            // print with aggregated data
            _invoicePrinter.PrintInvoice(invoiceEmployee.Name, invoiceCustomer.Address, amount);

            // store that the invoice has been printed
            await _commandBus.PublishAsync(new InvoiceSendCommand(InvoiceId.With(request.InvoiceId)),
                CancellationToken.None);
        }
    }
}