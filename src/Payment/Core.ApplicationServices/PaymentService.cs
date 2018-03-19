using EventFlow;
using Payment.Requests;

namespace Payment.Core.ApplicationServices
{
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