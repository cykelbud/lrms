using Microsoft.Extensions.DependencyInjection;

namespace Payout.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPayoutRegistrations(this IServiceCollection services)
        {
            //services.AddSingleton<IPaymentService, PaymentService>();

            return services;
        }

    }
}
