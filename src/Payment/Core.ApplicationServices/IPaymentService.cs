using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Requests;
using Payment.Response;

namespace Payment.Core.ApplicationServices
{
    public interface IPaymentService
    {
        Task SimulateReceivePayment(ReceivePaymentRequest request);
        Task<IEnumerable<PaymentDto>> GetAll();
        Task SimulatePaymentDue(PaymentDueRequest request);
        Task SetWaitingForPayment(WaitingForPaymentRequest request);
        Task<PaymentDto> GetPayment(Guid invoiceId);
        Task SimulateDebtCollection(Guid invoiceId);
        Task SimulatePaymentInjunction(Guid invoiceId);
        Task SimulateDistraint(Guid invoiceId);
    }
}
