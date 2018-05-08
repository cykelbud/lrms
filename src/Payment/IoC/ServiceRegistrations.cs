using Microsoft.Extensions.DependencyInjection;
using Payment.Core.ApplicationServices;

namespace Payment.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPaymentRegistrations(this IServiceCollection services)
        {
            services.AddTransient<IPaymentService, PaymentService>();

            return services;
        }

    }
}
