using Invoice.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddInvoiceRegistrations(IServiceCollection services)
        {
            services.AddSingleton<IInvoiceService, Core.ApplicationServices.InvoiceService>();

            return services;
        }

    }
}
