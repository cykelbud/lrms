using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using InvoiceService.Core.DomainModel;
using InvoiceService.Requests;

namespace InvoiceService.Core.ApplicationServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICommandBus _commandBus;

        public InvoiceService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
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
            await _commandBus.PublishAsync(new InvoiceSendCommand(InvoiceId.With(request.InvoiceId)),
                CancellationToken.None);
        }
    }
}