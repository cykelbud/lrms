using System;
using System.Threading.Tasks;
using InvoiceService.Requests;

namespace InvoiceService.Core.ApplicationServices
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoice(CreateInvoiceRequest request);
        Task SendInvoice(SendInvoiceRequest request);
    }
}
