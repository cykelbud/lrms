using System;
using System.Threading.Tasks;
using Invoice.Requests;

namespace Invoice.Core.ApplicationServices
{
    public interface IInvoiceService
    {
        Task<Guid> CreateInvoice(CreateInvoiceRequest request);
        Task SendInvoice(SendInvoiceRequest request);
    }
}
