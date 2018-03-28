using System.Threading.Tasks;
using Payment.Requests;
using Payment.Response;

namespace Payment.Core.ApplicationServices
{
    public interface IPaymentService
    {
        Task ReceivePayment(ReceivePaymentRequest request);
        Task<PaymentDto[]> GetAll();
        Task PaymentDue(PaymentDueRequest request);
        Task SetWaitingForPayment(WaitingForPaymentRequest request);
    }
}
