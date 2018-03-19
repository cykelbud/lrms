using Employee.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEmplyeeRegistrations(IServiceCollection services)
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();

            return services;
        }

    }
}
