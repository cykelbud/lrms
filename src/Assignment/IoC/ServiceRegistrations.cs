using Assignment.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddAssignmentRegistrations(this IServiceCollection services)
        {
            services.AddTransient<IAssignmentService, AssignmentService>();

            return services;
        }

    }
}
