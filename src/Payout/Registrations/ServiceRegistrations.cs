using Microsoft.Extensions.DependencyInjection;
using Payout.Core.ApplicationServices;

namespace Payout.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPayoutRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IPayoutService, PayoutService>();

            return services;
        }

    }
}
