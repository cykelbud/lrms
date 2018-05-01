using Invoice.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInvoiceRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IInvoiceService, InvoiceService>();
            services.AddSingleton<IInvoicePrinter, InvoicePrinter>();

            return services;
        }

    }
}
