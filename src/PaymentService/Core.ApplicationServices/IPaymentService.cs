using EventFlow;
using PaymentService.Requests;

namespace PaymentService.Core.ApplicationServices
{
    internal interface IPaymentService
    {
        void ReceivePayment(ReceivePaymentFromRequest request);
    }

    class PaymentService : IPaymentService
    {
        private readonly ICommandBus _commandBus;

        public PaymentService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        public void ReceivePayment(ReceivePaymentFromRequest request)
        {
           // _commandBus.PublishAsync(new )
        }
    }
}
