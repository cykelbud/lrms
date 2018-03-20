using Microsoft.Extensions.DependencyInjection;
using Payment.Core.ApplicationServices;

namespace Payment.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPaymentRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IPaymentService, PaymentService>();

            return services;
        }

    }
}
