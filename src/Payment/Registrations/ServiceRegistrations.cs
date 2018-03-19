using Microsoft.Extensions.DependencyInjection;
using Payment.Core.ApplicationServices;

namespace Payment.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInvoiceRegistrations(IServiceCollection services)
        {
            services.AddSingleton<IPaymentService, PaymentService>();

            return services;
        }

    }
}
