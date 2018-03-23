﻿using Assignment.Core.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddAssignmentRegistrations(this IServiceCollection services)
        {
            services.AddSingleton<IAssignmentService, AssignmentService>();

            return services;
        }

    }
}
