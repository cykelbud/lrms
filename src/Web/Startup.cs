using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Assignment.Registrations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Customer.Registrations;
using Employee.Registrations;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using Invoice.Registrations;
using Payment.Registrations;
using Payout.Registrations;
using Swashbuckle.AspNetCore.Swagger;
using Web.Projections;
using EmployeeReadModel = Web.Projections.EmployeeReadModel;

namespace Web
{
    public class Startup
    {

        public static Assembly WebAssembly { get; } = typeof(Startup).Assembly;
        public static Assembly AssignmentsAssembly { get; } = typeof(Assignment.Registrations.ServiceRegistrations).Assembly;
        public static Assembly CustomerAssembly { get; } = typeof(Customer.Registrations.ServiceRegistrations).Assembly;
        public static Assembly EmployeeAssembly { get; } = typeof(Employee.Registrations.ServiceRegistrations).Assembly;
        public static Assembly InvoiceAssembly { get; } = typeof(Invoice.Registrations.ServiceRegistrations).Assembly;
        public static Assembly PaymentAssembly { get; } = typeof(Payment.Registrations.ServiceRegistrations).Assembly;
        public static Assembly PayoutAssembly { get; } = typeof(Payout.Registrations.ServiceRegistrations).Assembly;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAssignmentRegistrations();
            services.AddCustomerRegistrations();
            services.AddEmplyeeRegistrations();
            services.AddInvoiceRegistrations();
            services.AddPaymentRegistrations();
            services.AddPayoutRegistrations();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            // EventFlow

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(builder) // Must be the first line!
                .AddDefaults(WebAssembly)
                .AddDefaults(AssignmentsAssembly)
                .AddDefaults(CustomerAssembly)
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

            this.ApplicationContainer = container;
            return new AutofacServiceProvider(this.ApplicationContainer);
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime)
        {

            app.UseStaticFiles();

            var migrator = ApplicationContainer.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(migrator);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

        }
    }
}
