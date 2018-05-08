using Employee.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEmplyeeRegistrations(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ICustomerService, CustomerService>();

            return services;
        }

    }
}
