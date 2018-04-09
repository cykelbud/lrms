using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using Swashbuckle.AspNetCore.Swagger;
using Web.Configuration;

namespace Web
{
    public class Startup
    {

      
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

            services.AddApplicationServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            this.ApplicationContainer = services.AddAutofacContainer();
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
