using Invoice.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInvoiceRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IInvoiceService, InvoiceService>();

            return services;
        }

    }
}
