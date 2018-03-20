using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Assignment.Registrations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Customer.Registrations;
using Employee.Registrations;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;
using Invoice.Registrations;
using Payment.Registrations;
using Payout.Registrations;
using Swashbuckle.AspNetCore.Swagger;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            //var container = EventFlowOptions.New
            //    .UseAutofacContainerBuilder(builder) // Must be the first line!
            //    .AddDefaults(DomainModelAssembly)
            //    .AddDefaults(DomainServicesAssembly)
            //    .AddDefaults(ReadModelAssembly)
            //    .AddDefaults(ProjectionsAssembly)
            //    .AddDefaults(QueryHandlerAssembly)
            //    .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(connectionString))
            //    .UseMssqlEventStore()
            //    .AddQueryHandlers(typeof(GetAllLoanApplicationsQueryHandler))
            //    .UseMssqlReadModel<LoanApplicationReadModel>()
            //    .CreateContainer();

            //this.ApplicationContainer = container;
            //return new AutofacServiceProvider(this.ApplicationContainer);
            return null;

        }

        public IContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
