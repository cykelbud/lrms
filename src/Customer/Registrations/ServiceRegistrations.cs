﻿using Customer.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddCustomerRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, CustomerService>();

            return services;
        }

    }
}
