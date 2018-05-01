using Employee.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEmplyeeRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<ICustomerService, CustomerService>();

            return services;
        }

    }
}
