using System.IO;
using System.Reflection;
using Assignment.IoC;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Employee.IoC;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;
using Invoice.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.IoC;
using Payout.IoC;
using Web.Projections;

namespace Web.Configuration
{
    public static class ServiceRegistrations
    {

        public static Assembly WebAssembly { get; } = typeof(Startup).Assembly;
        public static Assembly AssignmentsAssembly { get; } = typeof(Assignment.IoC.ServiceRegistrations).Assembly;
        public static Assembly EmployeeAssembly { get; } = typeof(Employee.IoC.ServiceRegistrations).Assembly;
        public static Assembly InvoiceAssembly { get; } = typeof(Invoice.IoC.ServiceRegistrations).Assembly;
        public static Assembly PaymentAssembly { get; } = typeof(Payment.IoC.ServiceRegistrations).Assembly;
        public static Assembly PayoutAssembly { get; } = typeof(Payout.IoC.ServiceRegistrations).Assembly;

        //public static IServiceCollection AddMappings(this IServiceCollection services)
        //{
        //    var profile = new MappingProfile();
        //    var cfg = new MapperConfigurationExpression();
        //    cfg.AddProfile(profile);
        //    var mapperConfig = new MapperConfiguration(cfg);
        //    var mapper = mapperConfig.CreateMapper();
        //    services.AddSingleton(mapper);

        //    return services;
        //}

        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddOptions();
            //services.Configure<AMLServiceOptions>(configuration.GetSection("AMLServiceOptions"));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAssignmentRegistrations();
            services.AddEmplyeeRegistrations();
            services.AddInvoiceRegistrations();
            services.AddPaymentRegistrations();
            services.AddPayoutRegistrations();

            return services;
        }

        public static IContainer AddAutofacContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            // EventFlow

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(builder) // Must be the first line!
                .AddDefaults(WebAssembly)
                .AddDefaults(AssignmentsAssembly)
                .AddDefaults(EmployeeAssembly)
                .AddDefaults(InvoiceAssembly)
                .AddDefaults(PaymentAssembly)
                .AddDefaults(PayoutAssembly)

                .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LrmsDb;Integrated Security=True;"))

                .UseMssqlEventStore()
                .UseMssqlReadModel<EmployeeReadModel>()
                .UseMssqlReadModel<CustomerReadModel>()
                .UseMssqlReadModel<InvoiceReadModel>()
                .UseMssqlReadModel<AssignmentReadModel>()
                .UseMssqlReadModel<PaymentReadModel>()

                .CreateContainer();

            return container;
        }


    }
}
