using System;
using System.Threading.Tasks;
using Invoice.Requests;
using Invoice.Response;

namespace Invoice.Core.ApplicationServices
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoice(CreateInvoiceRequest request);
        Task SendInvoice(SendInvoiceRequest request);
        Task SendReminder(Guid invoiceId);
        Task<InvoiceDto> GetInvoice(Guid invoiceId);
    }
}
