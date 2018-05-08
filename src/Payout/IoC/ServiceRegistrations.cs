using Microsoft.Extensions.DependencyInjection;
using Payout.Core.ApplicationServices;

namespace Payout.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPayoutRegistrations(this IServiceCollection services)
        {
            services.AddTransient<IPayoutService, PayoutService>();
            services.AddTransient<IMarginalenBank, MarginalenBank>();
            services.AddTransient<ISkatteverket, Skatteverket>();

            return services;
        }

    }
}
