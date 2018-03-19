using Payment.Requests;

namespace Payment.Core.ApplicationServices
{
    internal interface IPaymentService
    {
        void ReceivePayment(ReceivePaymentFromRequest request);
    }
}
